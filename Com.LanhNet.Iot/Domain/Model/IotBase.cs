using System;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Threading;
using System.Collections;
using Com.LanhNet.Iot.Infrastructure.Attributes;
using Com.LanhNet.Iot.Infrastructure.Helper;

namespace Com.LanhNet.Iot.Domain.Model
{
    internal class IotDisposeEventArgs : EventArgs
    {
        public Guid Id { get; set; }
        public bool Skip { get; set; } = false;
        public IotDisposeEventArgs(Guid id) : base()
        {
            Id = id;
        }
    }

    internal delegate void IotDisposeHandler(object sender, IotDisposeEventArgs args);

    public abstract class IotBase : IIot
    {
        internal static event IotDisposeHandler OnIotDispose;
        protected static AsyncLocal<eIotCommandType> CurrentCommandType;

        protected Guid _id;
        public Guid Id => _id;

        protected DateTime _onlineTime;
        public virtual bool IsOnline => (_onlineTime > DateTime.Now);

        public virtual bool IsVaild => true;

        protected IotBase(Guid id)
        {
            if (CurrentCommandType == null)
                CurrentCommandType = new AsyncLocal<eIotCommandType>();

            _id = id;
            _queue = new Queue();
            _event = new ManualResetEvent(true);
        }

        public virtual void Dispose()
        {
            IotDisposeEventArgs args = new IotDisposeEventArgs(Id);
            OnIotDispose?.Invoke(this, args);
            if (!args.Skip)
            {
                _queue.Clear();
                _event.Dispose();
                GC.SuppressFinalize(this);
            }
        }

        #region MessageQueue
        protected Queue _queue;
        protected ManualResetEvent _event;

        /// <summary>
        /// 等待消息
        /// </summary>
        /// <param name="millisecondsTimeout"></param>
        /// <returns></returns>
        protected bool Wait(out object message, int millisecondsTimeout = 33 * 1000)
        {
            message = null;
            if (eIotCommandType.Sync == IotBase.CurrentCommandType.Value)
            {
                _onlineTime = DateTime.Now.AddMilliseconds(millisecondsTimeout * 1.1);
                _event.Reset();
                if (_queue.Count <= 0)
                {
                    _event.WaitOne((int)(millisecondsTimeout * 0.9));
                }
                if (_queue.Count > 0)
                {
                    message = _queue.Dequeue();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        protected void Send(object message)
        {
            _queue.Enqueue(message);
            _event.Set();
        }

        /// <summary>
        /// 尝试获取消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected bool TryGet(out object message)
        {
            if (_queue.Count > 0)
            {
                message = _queue.Dequeue();
                return true;
            }
            message = null;
            return false;
        }
        #endregion

        #region Command
        /// <summary>
        /// 获取命令方法
        /// </summary>
        /// <param name="name">命令名</param>
        protected MethodInfo GetCommand(string name, eIotCommandType type)
        {
            MethodInfo[] methods = this.GetType().GetMethods();
            foreach(MethodInfo mi in methods)
            {
                IotCommandAttribute attr = (IotCommandAttribute)mi.GetCustomAttribute(typeof(IotCommandAttribute));
                if (null != attr && (attr.Type & type) != eIotCommandType.None)
                {
                    if (string.IsNullOrEmpty(attr.Name))
                    {
                        if (string.Equals(mi.Name, name, StringComparison.CurrentCultureIgnoreCase))
                            return mi;
                    }
                    else
                    {
                        if (string.Equals(attr.Name, name, StringComparison.CurrentCultureIgnoreCase))
                            return mi;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 调用命令
        /// </summary>
        /// <param name="method">命令方法</param>
        /// <param name="cmd">命令对象</param>
        protected JObject InvokeCommand(MethodInfo method, JObject cmd)
        {
            if (null == method)
                return IotResultHelper.Error;
            ParameterInfo[] parameters = method.GetParameters();
            object[] invokeParams = new object[parameters.Length];

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].Name == "context")
                    invokeParams[i] = cmd;
                else if (cmd.TryGetValue(parameters[i].Name, out JToken p))
                    invokeParams[i] = p.ToObject(parameters[i].ParameterType);
                else
                    invokeParams[i] = null;
            }
            return (JObject)method.Invoke(this, invokeParams);
        }
        #endregion

        #region Iot Api 
        public virtual JObject Sync(JObject cmd)
        {
            IotBase.CurrentCommandType.Value = eIotCommandType.Sync;

            JObject result;
            if (cmd.TryGetValue("cmd", out JToken name) &&
                name.Type == JTokenType.String)
            {
                MethodInfo method = GetCommand(name.Value<string>(), eIotCommandType.Sync);
                result = InvokeCommand(method, cmd);
            }
            else
            {
                result = IotResultHelper.Error;
            }
            return result;
        }

        public virtual JObject Update(JObject cmd)
        {
            IotBase.CurrentCommandType.Value = eIotCommandType.Update;

            JObject result;
            if (cmd.TryGetValue("cmd", out JToken name) &&
                name.Type == JTokenType.String)
            {
                MethodInfo method = GetCommand(name.Value<string>(), eIotCommandType.Update);
                result = InvokeCommand(method, cmd);
            }
            else
            {
                result = IotResultHelper.Error;
            }
            return result;
        }

        public virtual JObject Get(JObject cmd)
        {
            IotBase.CurrentCommandType.Value = eIotCommandType.Get;

            JObject result;
            if (cmd.TryGetValue("cmd", out JToken name) &&
                name.Type == JTokenType.String)
            {
                MethodInfo method = GetCommand(name.Value<string>(), eIotCommandType.Get);
                result = InvokeCommand(method, cmd);
            }
            else
            {
                result = IotResultHelper.Error;
            }
            return result;
        }

        public virtual JObject Set(JObject cmd)
        {
            IotBase.CurrentCommandType.Value = eIotCommandType.Set;

            JObject result;
            if (cmd.TryGetValue("cmd", out JToken name) &&
                name.Type == JTokenType.String)
            {
                MethodInfo method = GetCommand(name.Value<string>(), eIotCommandType.Set);
                result = InvokeCommand(method, cmd);
            }
            else
            {
                result = IotResultHelper.Error;
            }
            return result;
        }
        #endregion
    }
}

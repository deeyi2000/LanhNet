﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Com.LanhNet.Iot.Domain.Model;
using Com.LanhNet.Iot.Infrastructure.Exceptions;
using Com.LanhNet.Iot.Infrastructure.Attributes;

namespace Com.LanhNet.Iot.Infrastructure.Factories
{
    public class IotFactory : IIotFactory
    {
        protected class IotClient
        {
            public IIot Iot { get; set; } = null;
            public DateTime Lifetime { get; set; }
        }

        protected const int Type_Bytes = 4;
        protected IIotRepository _repository;
        protected Dictionary<Guid, IotClient> _dicIotClients;
        protected Dictionary<uint, Func<Guid, IIot>> _dicIotTypes;
        protected Timer _gcTimer;

        public IotFactory(IIotRepository repository = null, int gcInterval = (10 * 60 * 1000))
        {
            //IotBase.OnIotDispose += IotBase_OnIotDispose;
            _repository = repository;
            _dicIotClients = new Dictionary<Guid, IotClient>();
            _dicIotTypes = new Dictionary<uint, Func<Guid, IIot>>();
            _gcTimer = new Timer(_gcTimer_OnTimerCallback, null, 0, gcInterval);
        }

        /* 暂时用不上OnIotDispose事件
        private void IotBase_OnIotDispose(object sender, IotDisposeEventArgs args)
        {
            if (_dicIotClients.ContainsKey(args.Id))
                _dicIotClients.Remove(args.Id);
        }*/

        private void _gcTimer_OnTimerCallback(object state)
        {
            lock (_dicIotClients)
            {
                var ids = new Guid[_dicIotClients.Count];
                _dicIotClients.Keys.CopyTo(ids, 0);

                foreach (var id in ids)
                {
                    var c = _dicIotClients[id];
                    if (c.Lifetime < DateTime.Now)
                    {
                        _dicIotClients.Remove(id);
                        c.Iot.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// 注册Iot类型
        /// </summary>
        /// <param name="type">Iot类型</param>
        public void RegisterClientType(Type type, Func<Guid, IIot> constructor = null)
        {
            if (!typeof(IIot).IsAssignableFrom(type))
                throw (new TypeErrorException());

            IotAttribute attr = (IotAttribute)type.GetCustomAttributes(typeof(IotAttribute), false)[0];

            if (_dicIotTypes.ContainsKey(attr.Id))
                throw (new SameTypeIdException());

            if (null != constructor)
                _dicIotTypes.Add(attr.Id, constructor);
            else if (null != _repository)
            {
                MethodInfo mi = typeof(IIotRepository).GetMethod("Load").MakeGenericMethod(type);
                _dicIotTypes.Add(attr.Id, (id) => (IIot)mi.Invoke(_repository, new object[] { id }));
            }
            else
                _dicIotTypes.Add(attr.Id, (id) => (IIot)Activator.CreateInstance(type, new object[] { id }));
        }

        /// <summary>
        /// 获取Iot
        /// </summary>
        /// <param name="id">Iot Id</param>
        public IIot GetIot(Guid id)
        {
            if (!_dicIotClients.TryGetValue(id, out IotClient client))
            {
                byte[] byteId = id.ToByteArray();
                uint typeId = BitConverter.ToUInt32(byteId, 0);

                if (!_dicIotTypes.TryGetValue(typeId, out Func<Guid, IIot> clientConstructor))
                    return null;

                client = new IotClient()
                {
                    Iot = clientConstructor.Invoke(id)
                };
                lock (_dicIotClients)
                {
                    _dicIotClients.Add(id, client);
                }
            }

            client.Lifetime = DateTime.Now.AddMinutes(10.0);
            return client.Iot;
        }
    }
}

using System;
using Newtonsoft.Json.Linq;

namespace Com.LanhNet.Iot.Domain.Model
{
    public interface IIot : IDisposable
    {
        /// <summary>
        /// 获取Iot是否有效
        /// </summary>
        bool IsVaild { get; }

        /// <summary>
        /// 获取Iot是否在线
        /// </summary>
        bool IsOnline { get; }

        #region Api
        /// <summary>
        /// Sync接口
        /// </summary>
        /// <param name="cmd">命令主体</param>
        JObject Sync(JObject cmd);

        /// <summary>
        /// Update接口
        /// </summary>
        /// <param name="cmd">命令主体</param>
        JObject Update(JObject cmd);
        #endregion

        #region Client
        /// <summary>
        /// Get接口
        /// </summary>
        /// <param name="cmd">命令主体</param>
        /// <returns></returns>
        JObject Get(JObject cmd);

        /// <summary>
        /// Set接口
        /// </summary>
        /// <param name="cmd">命令主体</param>
        /// <returns></returns>
        JObject Set(JObject cmd);
        #endregion
    }
}

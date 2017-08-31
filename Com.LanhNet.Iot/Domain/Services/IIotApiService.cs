using System;
using Newtonsoft.Json.Linq;

namespace Com.LanhNet.Iot.Domain.Services
{
    public interface IIotApiService
    {
        /// <summary>
        /// Sync Api
        /// </summary>
        /// <param name="id">Iot id</param>
        /// <param name="cmd"></param>
        JObject Sync(Guid id, JObject cmd);

        /// <summary>
        /// Update Api
        /// </summary>
        /// <param name="id">Iot id</param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        JObject Update(Guid id, JObject cmd);
        /// <summary>
        /// Get Api
        /// </summary>
        /// <param name="id">Iot id</param>
        /// <param name="cmd"></param>
        JObject Get(Guid id, JObject cmd);

        /// <summary>
        /// Set Api
        /// </summary>
        /// <param name="id">Iot id</param>
        /// <param name="cmd"></param>
        /// <returns></returns>
        JObject Set(Guid id, JObject cmd);
    }
}

using System;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Model;
using Com.LanhNet.Iot.Infrastructure.Attributes;
using Com.LanhNet.Iot.Infrastructure.Helper;

namespace Com.LanhNet.Iot.WepApi.Domain.Models
{
    [Iot(0xB9B3BCFD)]
    public class LevePowerIot : IotBase
    {
        IIotRepository _repository;
        public LevePowerIot(Guid id, IIotRepository repository) : base(id)
        {
            _repository = repository;
        }

        [IotCommand(Type = eIotCommandType.Sync)]
        public JObject Tick(JObject context)
        {
            var msg = (string)Wait();
            if (null != msg)
                return IotResultHelper.Parse(msg, eIotResultType.OK);
            return IotResultHelper.OK;
        }

        [IotCommand(Type = eIotCommandType.Set)]
        public JObject Unlock()
        {
            Send("{action:'unlock'}");
            return IotResultHelper.OK;
        }

        [IotCommand(Type = eIotCommandType.Set)]
        public JObject Lock()
        {
            Send("{action:'lock'}");
            return IotResultHelper.OK;
        }
    }
}

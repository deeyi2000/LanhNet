using System;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Model;
using Com.LanhNet.Iot.Infrastructure.Attributes;
using Com.LanhNet.Iot.Infrastructure.Helper;

namespace Com.LanhNet.Iot.WepApi.Domain.Models
{
    public enum eLevePowerIotState
    {
        Lock,
        Unlock
    }

    [Iot(0xB9B3BCFD)]
    public class LevePowerIot : IotBase
    {
        IIotRepository _repository;

        protected eLevePowerIotState _state = eLevePowerIotState.Lock;
        protected double _longitude;
        protected double _latitude;
        protected float _voltage;

        public LevePowerIot(Guid id, IIotRepository repository) : base(id)
        {
            _repository = repository;
        }

        [IotCommand(Type = eIotCommandType.Sync)]
        public JObject Tick(eLevePowerIotState sta, double lon, double lat, float vol)
        {
            var state = sta; //sta.ToLower() == "lock" ? eLevePowerIotState.Lock : eLevePowerIotState.Unlock;
            _longitude = lon; //Convert.ToDouble(lon);
            _latitude = lat; //Convert.ToDouble(lat);
            _voltage = vol; //Convert.ToSingle(vol);

            var msg = new object();
            if(TryGet(out msg))
            {
                return IotResultHelper.Parse((string)msg, eIotResultType.OK);
            }
            else if(state != _state)
            {
                msg = _state == eLevePowerIotState.Lock ? "{action:'lock'}" : "{action:'unlock'}";
                return IotResultHelper.Parse((string)msg, eIotResultType.OK);
            }
            else
            {
                if(Wait(out msg))
                    return IotResultHelper.Parse((string)msg, eIotResultType.OK);
            }
            return IotResultHelper.OK;
        }

        [IotCommand(Type = eIotCommandType.Set)]
        public JObject Unlock()
        {
            if (eLevePowerIotState.Unlock != _state)
            {
                _state = eLevePowerIotState.Unlock;
                Send("{action:'unlock'}");
            }
            return IotResultHelper.OK;
        }

        [IotCommand(Type = eIotCommandType.Set)]
        public JObject Lock()
        {
            if (eLevePowerIotState.Lock != _state)
            {
                _state = eLevePowerIotState.Lock;
                Send("{action:'lock'}");
            }
            return IotResultHelper.OK;
        }
    }
}

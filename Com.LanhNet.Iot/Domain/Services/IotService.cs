using System;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Model;
using Com.LanhNet.Iot.Infrastructure.Helper;

namespace Com.LanhNet.Iot.Domain.Services
{
    public class IotService : IIotApiService, IIotManageService
    {
        private IIotRepository _repository;
        private IIotFactory _factory;
        

        public IotService(IIotRepository repository, IIotFactory factory)
        {
            _repository = repository;
            _factory = factory;
        }

        #region IIotApiService
        public JObject Sync(Guid id, JObject cmd)
        {
            JObject result;
            IIot iot = _factory.GetIot(id);
            if (null != iot)
                result = iot.Sync(cmd);
            else
                result = IotResultHelper.Error;
            return result;
        }

        public JObject Update(Guid id, JObject cmd)
        {
            JObject result;
            IIot iot = _factory.GetIot(id);
            if (null != iot)
                result = iot.Update(cmd);
            else
                result = IotResultHelper.Error;
            return result;
        }

        public JObject Get(Guid id, JObject cmd)
        {
            JObject result;
            IIot iot = _factory.GetIot(id);
            if (null != iot)
                result = iot.Get(cmd);
            else
                result = IotResultHelper.Error;
            return result;
        }

        public JObject Set(Guid id, JObject cmd)
        {
            JObject result;
            IIot iot = _factory.GetIot(id);
            if (null != iot)
                result = iot.Set(cmd);
            else
                result = IotResultHelper.Error;
            return result;
        }
        #endregion

        #region IIotManageService
        public IIot Add(IIot iot)
        {
            return _repository.Add<IIot>(iot);
        }

        public IIot Edit(IIot iot)
        {
            return _repository.Edit<IIot>(iot);
        }

        public void Remove(IIot iot)
        {
            _repository.Remove<IIot>(iot);
        }

        public IIot Find(Guid id)
        {
            return _repository.Find<IIot>(id);
        }
        #endregion
    }
}

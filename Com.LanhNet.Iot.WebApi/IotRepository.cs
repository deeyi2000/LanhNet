using System;
using System.Collections.Generic;
using System.Text;

namespace Com.LanhNet.Iot.Domain.Model
{
    public class IotRepository : IIotRepository
    {
        public IotRepository() { }

        public T Add<T>(T iot)
        {
            throw new NotImplementedException();
        }

        public T Edit<T>(T iot)
        {
            throw new NotImplementedException();
        }

        public T Find<T>(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T iot)
        {
            throw new NotImplementedException();
        }

        public T Load<T>(Guid id)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { id, this });
        }

        public void Save<T>(T iot)
        {
            throw new NotImplementedException();
        }
    }
}

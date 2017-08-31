using System;
using System.Collections.Generic;
using System.Text;

namespace Com.LanhNet.Iot.Domain.Model
{
    public interface IIotRepository
    {
        T Find<T>(Guid id);
        T Add<T>(T iot);
        T Edit<T>(T iot);
        void Remove<T>(T iot);

        T Load<T>(Guid id);
        void Save<T>(T iot);
    }
}

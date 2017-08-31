using Com.LanhNet.Iot.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.LanhNet.Iot.Domain.Services
{
    public interface IIotManageService
    {
        IIot Add(IIot iot);
        IIot Edit(IIot iot);
        void Remove(IIot iot);
        IIot Find(Guid id);
    }
}

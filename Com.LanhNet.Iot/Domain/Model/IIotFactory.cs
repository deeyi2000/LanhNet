using System;
using System.Collections.Generic;
using System.Text;

namespace Com.LanhNet.Iot.Domain.Model
{
    public interface IIotFactory
    {
        IIot GetIot(Guid id);
    }
}

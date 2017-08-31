using System;

namespace Com.LanhNet.Iot.Infrastructure.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class IotAttribute : Attribute
    {
        private const int Type_Bytes = 4;
        public uint Id { get; private set; }

        public IotAttribute(uint id)
        {
            Id = id;
        }
    }
}

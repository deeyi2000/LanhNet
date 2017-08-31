using System;

namespace Com.LanhNet.Iot.Infrastructure.Attributes
{
    public enum eIotCommandType
    {
        None = 0x00,
        Sync = 0x01,
        Update = 0x02,
        Api = 0x03,
        Get = 0x04,
        Set = 0x08,
        Client = 0x0C,
        All = 0x0F
    }

    [AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class IotCommandAttribute : Attribute
    {
        public string Name { get; set; }
        public eIotCommandType Type { get; set; } = eIotCommandType.All;

        public IotCommandAttribute() { }
    }
}

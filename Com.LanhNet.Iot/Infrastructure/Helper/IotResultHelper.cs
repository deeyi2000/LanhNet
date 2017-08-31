using Newtonsoft.Json.Linq;

namespace Com.LanhNet.Iot.Infrastructure.Helper
{
    public enum eIotResultType
    {
        OK,
        Error
    }

    public class IotResultHelper
    {
        public static JObject OK
        {
            get
            {
                return JObject.Parse(@"{result:'ok'}");
            }
        }
        public static JObject Error
        {
            get
            {
                return JObject.Parse(@"{result:'error'}");
            }
        }
        public static JObject Offline
        {
            get
            {
                return JObject.Parse(@"{result:'offline'}");
            }
        }

        public static JObject Parse(string json, eIotResultType type)
        {
            JObject result = JObject.Parse(json);
            switch(type)
            {
                case eIotResultType.OK: result.AddFirst(new JProperty("result", "ok")); break;
                default: result.AddFirst(new JProperty("result", "error")); break;
            }
            return result;
        }

        public static JObject Parse(object obj, eIotResultType type)
        {
            JObject result = JObject.FromObject(obj);
            switch (type)
            {
                case eIotResultType.OK: result.AddFirst(new JProperty("result", "ok")); break;
                default: result.AddFirst(new JProperty("result", "error")); break;
            }
            return result;
        }
    }
}

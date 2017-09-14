using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;
using Com.LanhNet.Iot.Infrastructure.Helper;
using Com.LanhNet.Iot.WepApi.Infrastructure.common;

namespace Com.LanhNet.Iot.WepApi.Controllers
{
    [Route("api/v1/[controller]")]
    public class SyncController : Controller
    {
        protected IIotApiService _service;

        public SyncController(IIotApiService service)
        {
            _service = service;
        }

        // GET api/sync/{id}/{cmd}
        [HttpGet("{id}/{cmd}")]
        public string Get(Guid id, string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Sync(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }

        // POST api/sync/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Sync(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;
using Com.LanhNet.Iot.Infrastructure.Helper;
using Com.LanhNet.Iot.WebApi.Infrastructure.common;

namespace Com.LanhNet.Iot.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UpdateController : Controller
    {
        protected IIotApiService _service;

        public UpdateController(IIotApiService service)
        {
            _service = service;
        }

        // GET api/update/{id}/{cmd}
        [HttpGet("{id}/{cmd}")]
        public string Get(Guid id, string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Update(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }

        // POST api/update/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Update(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }
    }
}

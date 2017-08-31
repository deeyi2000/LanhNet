using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;

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
            return _service.Sync(id, JObject.Parse(cmd)).ToString();
        }

        // POST api/sync/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            return _service.Sync(id, JObject.Parse(cmd)).ToString();
        }
    }
}

using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;

namespace Com.LanhNet.Iot.WepApi.Controllers
{
    [Route("api/v1/[controller]")]
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
            return _service.Update(id, JObject.Parse(cmd)).ToString();
        }

        // POST api/update/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            return _service.Update(id, JObject.Parse(cmd)).ToString();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Com.LanhNet.Iot.Domain.Services;
using Com.LanhNet.Iot.Infrastructure.Helper;
using Com.LanhNet.Iot.WebApi.Infrastructure.common;

namespace Com.LanhNet.Iot.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class GetController : Controller
    {
        protected IIotApiService _service;

        public GetController(IIotApiService service)
        {
            _service = service;
        }

        // GET api/get/{id}/{cmd}
        [HttpGet("{id}/{cmd}")]
        public string Get(Guid id, string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Get(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }

        // POST api/get/{id}
        [HttpPost("{id}")]
        public string Post(Guid id, [FromBody]string cmd)
        {
            try
            {
                cmd = Base64Encoder.Decode(cmd);
                string result = _service.Get(id, JObject.Parse(cmd)).ToString();
                return Base64Encoder.Encode(result);
            }
            catch
            {
                return Base64Encoder.Encode(IotResultHelper.Error.ToString());
            }
        }
    }
}

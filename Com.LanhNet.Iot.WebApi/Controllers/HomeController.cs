using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Com.LanhNet.Iot.Domain.Services;
using Com.LanhNet.Iot.WebApi.Infrastructure.common;

namespace Com.LanhNet.Iot.WebApi.Controllers
{
    public class HomeController : Controller
    {
        private IIotManageService _service;

        public HomeController(IIotManageService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            string test = "{cmd:'tick'},{";

            test = Base64Encoder.Encode(test);
            string tmp = Base64Encoder.Decode(test);

            return View();
        }
    }
}
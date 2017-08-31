using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Com.LanhNet.Iot.Domain.Services;

namespace Com.LanhNet.Iot.WepApi.Controllers
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
            return View();
        }
    }
}
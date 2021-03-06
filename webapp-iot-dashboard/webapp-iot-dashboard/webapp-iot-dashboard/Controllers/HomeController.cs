﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapp_iot_dashboard.Model;
using vunvulea_iot_core;

namespace webapp_iot_dashboard.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            SystemStatus systemStatus = new SystemStatus();

            HomeModel homeModel = new HomeModel()
            {
                CurrentTemperature = await systemStatus.GetCurrentSystemStatusAsync(SystemStatusType.CurrentTemperature),
                MinimumTemperature = await systemStatus.GetCurrentSystemStatusAsync(SystemStatusType.MinimumTemperature),
                AlarmStatus = bool.Parse(await systemStatus.GetCurrentSystemStatusAsync(SystemStatusType.MotionDetectorOn)),
            };

            return View(homeModel);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

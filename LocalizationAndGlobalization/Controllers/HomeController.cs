﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LocalizationAndGlobalization.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace LocalizationAndGlobalization.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHtmlLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger,
                IHtmlLocalizer<HomeController> localizer)
        {
            _logger = logger;
            this._localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["HelloWorld"]= _localizer["HelloWorld"];
            return View();
        }
        [HttpPost]
        public IActionResult CultureManagement(string culture , string returnUrl)
        {
            //manage culture by cookies
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                             CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                             new CookieOptions { Expires = DateTimeOffset.Now.AddDays(10) });
            if (returnUrl !=null)
            {
                return LocalRedirect(returnUrl);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

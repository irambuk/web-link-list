using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebLinkList.WebMvc.Models;
using WebLinkList.EF;
using WebLinkList.Common;
using Microsoft.Extensions.Options;

namespace WebLinkList.WebMvc.Controllers
{
    public class WebLinkController : Controller
    {
        private WebLinkContext _context;
        private ConfigSettings _configSettings;

        public WebLinkController(WebLinkContext context, IOptions<ConfigSettings> configSettingsOptions)
        {
            _context = context;
            _configSettings = configSettingsOptions.Value;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Your application description page.";

            var webLinks = _context.WebLinks.OrderByDescending(wl => wl.CreatedDateTime).ToList();
            return View(webLinks);
        }
    }
}

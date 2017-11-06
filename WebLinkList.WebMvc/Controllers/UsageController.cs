using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLinkList.Common;
using WebLinkList.EF;
using Microsoft.Extensions.Options;

namespace WebLinkList.WebMvc.Controllers
{
    public class UsageController : Controller
    {
        private WebLinkContext _context;
        private ConfigSettings _configSettings;

        public UsageController(WebLinkContext context, IOptions<ConfigSettings> configSettingsOptions)
        {
            _context = context;
            _configSettings = configSettingsOptions.Value;
        }

        public IActionResult Index()
        {           

            var usages = _context.Usages.OrderByDescending(wl => wl.CreatedDateTime).ToList();
            return View(usages);
        }
    }
}
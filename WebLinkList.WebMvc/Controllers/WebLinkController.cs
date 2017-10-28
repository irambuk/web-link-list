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

            var maxCount = _configSettings.SpecialPanelListMaxSize;

            var homeVm = new HomeViewModel();
            homeVm.FaviouriteWebLinks = _context.WebLinks
                .Where(wl => wl.IsFaviourite)
                .OrderByDescending(wl => wl.CreatedDateTime)
                .Select(wl => new WebLinkViewModel {
                    WebLinkId = wl.Id,
                    Name = wl.Name, Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name}).ToList()
                })
                .Take(maxCount)
                .ToList();

            homeVm.TopWebLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel { WebLinkId = wl.Id, Name = wl.Name, Url = wl.Url, CurrentCount = wl.Usages.Count(), LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue }).OrderByDescending(wl => wl.CurrentCount).Take(maxCount).ToList();

            homeVm.MostRecentlyViewedLinks = _context.WebLinks
                .Where(wl => wl.Usages.Any())
                .OrderByDescending(wl => wl.Usages.Max(u => u.CreatedDateTime))
                .Select(wl => new WebLinkViewModel {
                    WebLinkId = wl.Id,
                    Name = wl.Name, Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .Take(maxCount)
                .ToList();

            homeVm.LeastRecentlyViewedLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel {
                    WebLinkId = wl.Id,
                    Name = wl.Name, Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .OrderByDescending(wl => wl.CurrentCount)
                .Take(maxCount)
                .ToList();

            homeVm.WebLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel {
                    WebLinkId = wl.Id,
                    Name = wl.Name, Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .ToList();

            homeVm.Categories = _context.Categories
                .Select(c => new CategoryViewModel {CategoryId = c.Id, Name = c.Name, CurrentCount = c.WebLinkCategories.Count() }).OrderByDescending(c => c.CurrentCount)
                .ToList();
            return View(homeVm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

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
using WebLinkList.EF.Model;
using System.Drawing;

namespace WebLinkList.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        private WebLinkContext _context;
        private ConfigSettings _configSettings;

        public HomeController(WebLinkContext context, IOptions<ConfigSettings> configSettingsOptions)
        {
            _context = context;
            _configSettings = configSettingsOptions.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HomeViewModel homeVm = PrepareViewModel(null);
            return View(homeVm);
        }
             

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm]string searchText)
        {
            HomeViewModel homeVm = PrepareViewModel(searchText);
            return View(homeVm);
        }

        [HttpGet]
        public IActionResult Redirect(Guid id) {

            var webLink = _context.WebLinks.FirstOrDefault(wl => wl.Id == id);

            if (webLink == null || string.IsNullOrEmpty(webLink.Url)) {
                return View("Index");
            }

            //record the usage
            var usage = new Usage { Id = Guid.NewGuid(), CreatedDateTime = DateTime.Now, WebLinkId = id};
            _context.Usages.Add(usage);
            _context.SaveChanges();
            
            //redirect
            return Redirect(webLink.Url);
        }

        public IActionResult Category(Guid id, UsageDropDownTypes? type)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null || string.IsNullOrEmpty(category.Name))
            {
                return View("Index");
            }

            var usageType = (type.HasValue) ? type.Value : UsageDropDownTypes.LastMonth;

            var viewModel = new CategoryHomeViewModel(usageType) { Category = category};
            viewModel.WebLinks = _context.WebLinks.Where(wl => wl.WebLinkCategories.Any(c => c.CategoryId == id))
                .OrderByDescending(wl => wl.CreatedDateTime)
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .ToList();

            var usages = _context.Usages.Where(u => u.WebLink.WebLinkCategories.Any(wlc => wlc.CategoryId == id)).ToList();
            viewModel.Usages = usages;


            var usageData = new List<UsageDataPerUnitViewModel>();

            if (usages != null && usages.Count != 0)
            {
                foreach (var date in viewModel.UsageTimelineDates)
                {
                    var usageCount = usages.Where(u => u.CreatedDateTime.Date == date.Date).Count();
                    var usageDataPerUnitViewModel = new UsageDataPerUnitViewModel { NoOfVisits = usageCount, UnitName = date.ToShortTimeString(), SelectedColor = Color.Blue };
                    usageData.Add(usageDataPerUnitViewModel);
                }
            }

            viewModel.UsageData = usageData;





            return View(viewModel);
        }

        public IActionResult WebLink(Guid id, UsageDropDownTypes? type)
        {
            var weblink = _context.WebLinks.FirstOrDefault(wl => wl.Id == id);
            if (weblink == null)
            {
                return View("Index");
            }

            var usageType = (type.HasValue) ? type.Value : UsageDropDownTypes.LastMonth;

            var weblinkHomeViewModel = new WebLinkHomeViewModel(usageType)
            {
                WebLink = weblink,
                Usages = _context.Usages.Where(u => u.WebLinkId == id).OrderByDescending(wl => wl.CreatedDateTime).ToList()
            };


            return View(weblinkHomeViewModel);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private HomeViewModel PrepareViewModel(string searchText)
        {
            var maxCount = _configSettings.SummaryPanelListMaxSize;

            var homeVm = new HomeViewModel();
            homeVm.CurrentSearchString = searchText;
            homeVm.FaviouriteWebLinks = _context.WebLinks
                .Where(wl => wl.IsFaviourite)
                .OrderByDescending(wl => wl.CreatedDateTime)
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .Take(maxCount)
                .ToList();

            homeVm.TopWebLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64
                })
                    .OrderByDescending(wl => wl.CurrentCount)
                    .Take(maxCount)
                    .ToList();

            homeVm.MostRecentlyViewedLinks = _context.WebLinks
                .Where(wl => wl.Usages.Any())
                .OrderByDescending(wl => wl.Usages.Max(u => u.CreatedDateTime))
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .Take(maxCount)
                .ToList();

            homeVm.LeastRecentlyViewedLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .OrderBy(wl => wl.CurrentCount)
                .Take(maxCount)
                .ToList();

            if (string.IsNullOrEmpty(searchText))
            {
                homeVm.WebLinks = _context.WebLinks
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .ToList();
            }
            else
            {
                homeVm.WebLinks = _context.WebLinks
                .Where(wl => wl.Name.Contains(searchText) || wl.Url.Contains(searchText) )
                .Select(wl => new WebLinkViewModel
                {
                    WebLinkId = wl.Id,
                    Name = wl.Name,
                    Url = wl.Url,
                    CurrentCount = wl.Usages.Count(),
                    LastVisitedDateTime = (wl.Usages.Any()) ? wl.Usages.Max(u => u.CreatedDateTime) : DateTime.MinValue,
                    ImageBase64 = wl.FaviconImageBase64,
                    Categories = wl.WebLinkCategories.Select(c => new CategoryViewModel { CategoryId = c.Category.Id, Name = c.Category.Name }).ToList()
                })
                .ToList();
            }

            

            homeVm.Categories = _context.Categories
                .Select(c => new CategoryViewModel { CategoryId = c.Id, Name = c.Name, CurrentCount = c.WebLinkCategories.Count() }).OrderByDescending(c => c.CurrentCount)
                .ToList();
            return homeVm;
        }
    }
}

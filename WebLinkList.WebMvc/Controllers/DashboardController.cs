using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLinkList.EF;
using WebLinkList.Common;
using Microsoft.Extensions.Options;
using WebLinkList.WebMvc.Models;
using System.Drawing;

namespace WebLinkList.WebMvc.Controllers
{
    public class DashboardController : Controller
    {
        private WebLinkContext _context;
        private ConfigSettings _configSettings;

        public DashboardController(WebLinkContext context, IOptions<ConfigSettings> configSettingsOptions)
        {
            _context = context;
            _configSettings = configSettingsOptions.Value;
        }


        public ActionResult Index(UsageDropDownTypes? type)
        {

            var usageType = (type.HasValue) ? type.Value : UsageDropDownTypes.LastWeek;

            var dashboardVm = new DashboardViewModel(usageType);
            var startDateTime = DateTime.MinValue;
            if (type.HasValue) {
                startDateTime = dashboardVm.SelectedUsageDropDownViewModel.GetCalculatedStartDate();
            }

            var categories = _context.Categories.OrderBy(c => c.CreatedDateTime).ToList();//avoiding the group-by, revisit here
            

            foreach (var category in categories)
            {
                int count = 0;

                if (type == null)
                {
                    count = _context.Usages.Where(u => u.WebLink.WebLinkCategories.Any(wc => wc.CategoryId == category.Id)).Count();
                }
                else
                {
                    count = _context.Usages.Where(u => u.CreatedDateTime > startDateTime && u.WebLink.WebLinkCategories.Any(wc => wc.CategoryId == category.Id)).Count();
                }
                dashboardVm.UsageData.Add(new UsageDataPerUnitViewModel { UnitName = category.Name, NoOfVisits = count, SelectedColor = category.GraphColor});
            }

            return View(dashboardVm);
        }

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Export(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
﻿using System;
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
            var dashboardVm = new DashboardViewModel();
            var startDateTime = DateTime.MinValue;
            if (type.HasValue) {
                dashboardVm.SelectType(type.Value);
                startDateTime = dashboardVm.GetCalculatedStartDate(type.Value);
            }

            var categories = _context.Categories.OrderBy(c => c.CreatedDateTime).ToList();//avoiding the group-by, revisit here

            foreach (var category in categories)
            {
                int count = 0;

                if (type == null)
                {
                    count = _context.Usages.Count(u => u.WebLink.WebLinkCategories.Any(wc => wc.CategoryId == category.Id));
                }
                else
                {
                    count = _context.Usages.Where(u => u.CreatedDateTime > startDateTime).Count(u => u.WebLink.WebLinkCategories.Any(wc => wc.CategoryId == category.Id));
                }
                dashboardVm.UsageDataPerCategoryViewModels.Add(new UsageDataPerCategoryViewModel { CategoryName = category.Name, NoOfVisits = count, SelectedColor = Color.PaleVioletRed});
            }

            return View(dashboardVm);
        }
    }
}
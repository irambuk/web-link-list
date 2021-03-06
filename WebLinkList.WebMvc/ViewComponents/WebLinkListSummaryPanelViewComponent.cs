﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.WebMvc.Models;

namespace WebLinkList.WebMvc.ViewComponents
{
    [ViewComponent(Name = "WebLinkListSummaryPanel")]
    public class WebLinkListSummaryPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title, List<WebLinkViewModel> webLinks)
        {
            ViewData["Title"] = title;
            return View(webLinks);
        }
    }
}

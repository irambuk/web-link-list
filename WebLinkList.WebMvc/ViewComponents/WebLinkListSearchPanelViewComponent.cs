using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.WebMvc.Models;

namespace WebLinkList.WebMvc.ViewComponents
{
    [ViewComponent(Name = "WebLinkListSearchPanel")]
    public class WebLinkListSearchPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            ViewData["Title"] = title;
            return View();
        }
    }
}

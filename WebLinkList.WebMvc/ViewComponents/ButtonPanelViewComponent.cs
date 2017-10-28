using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLinkList.WebMvc.ViewComponents
{
    [ViewComponent(Name = "ButtonPanel")]
    public class ButtonPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title)
        {
            ViewData["Title"] = title;
            return View();
        }
    }
}

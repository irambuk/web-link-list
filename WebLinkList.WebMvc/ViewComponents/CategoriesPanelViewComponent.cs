using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLinkList.WebMvc.Models;

namespace WebLinkList.WebMvc.ViewComponents
{
    [ViewComponent(Name = "CategoriesPanel")]
    public class CategoriesPanelViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string title, List<CategoryViewModel> categories)
        {
            ViewData["Title"] = title;
            return View(categories);
        }
    }
}

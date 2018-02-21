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
using System.Net.Http;

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

        public ActionResult Details(Guid id)
        {
            var webLink = _context.WebLinks.FirstOrDefault(c => c.Id == id);
            var newWebLinkViewModel = new WebLinkModifyViewModel { WebLink = webLink };
            newWebLinkViewModel.CategoryItems = _context.Categories.OrderBy(c => c.Name).Select(c => new CategoryItem { Id = c.Id, Name = c.Name, IsSelected = c.WebLinkCategories.Any(wc => wc.WebLinkId == id) }).ToList();

            if (webLink != null)
            {
                return View(newWebLinkViewModel);
            }
            return View("Index");
        }

        public ActionResult Create()
        {
            var newWebLink = new WebLink { Id = Guid.NewGuid(), CreatedDateTime = DateTime.Now };
            var newWebLinkViewModel = new WebLinkModifyViewModel { WebLink = newWebLink };
            newWebLinkViewModel.CategoryItems = _context.Categories.OrderBy(c => c.Name).Select(c => new CategoryItem { Id = c.Id, Name = c.Name, IsSelected = false}).ToList();
            return View(newWebLinkViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] WebLinkModifyViewModel webLinkViewModel)
        {
            try
            {
                var webLink = webLinkViewModel.WebLink;
                webLink.Id = Guid.NewGuid();
                webLink.CreatedDateTime = DateTime.Now;
                webLink.FaviconImageBytes = await LoadFavIcon(webLink.Url);

                _context.WebLinks.Add(webLink);
                _context.SaveChanges();

                //links
                foreach (var category in webLinkViewModel.CategoryItems)
                {
                    if (!category.IsSelected)
                    {
                        continue;
                    }

                    var webLinkCategory = new WebLinkCategory { WebLinkId = webLink.Id, CategoryId = category.Id };
                    _context.WebLinkCategories.Add(webLinkCategory);
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(Guid id)
        {
            var webLink = _context.WebLinks.FirstOrDefault(c => c.Id == id);
            var newWebLinkViewModel = new WebLinkModifyViewModel { WebLink = webLink };
            newWebLinkViewModel.CategoryItems = _context.Categories.OrderBy(c => c.Name).Select(c => new CategoryItem { Id = c.Id, Name = c.Name, IsSelected = c.WebLinkCategories.Any(wc => wc.WebLinkId == id) }).ToList();

            if (newWebLinkViewModel != null)
            {
                return View(newWebLinkViewModel);
            }
            return View("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, [FromForm] WebLinkModifyViewModel webLinkViewModel)
        {
            try
            {
                var webLink = webLinkViewModel.WebLink;
                var originalWebLink = _context.WebLinks.FirstOrDefault(c => c.Id == id);

                if (originalWebLink.Url != webLink.Url || originalWebLink.FaviconImageBase64 == string.Empty)
                {
                    originalWebLink.FaviconImageBytes = await LoadFavIcon(webLink.Url);
                }

                originalWebLink.Name = webLink.Name;
                originalWebLink.IsFaviourite = webLink.IsFaviourite;
                originalWebLink.IsSingleReadLink = webLink.IsSingleReadLink;
                originalWebLink.Url = webLink.Url;
                _context.SaveChanges();

                //links
                foreach (var category in webLinkViewModel.CategoryItems)
                {
                    var existingCategory = _context.WebLinkCategories.FirstOrDefault(wc => wc.WebLinkId == id && wc.CategoryId == category.Id);

                    if (existingCategory != null)
                    {
                        if (category.IsSelected)
                        {
                            continue;
                        }

                        //remove
                        _context.WebLinkCategories.Remove(existingCategory);
                    }
                    else
                    {
                        if (!category.IsSelected)
                        {
                            continue;
                        }

                        //add
                        var webLinkCategory = new WebLinkCategory { WebLinkId = id, CategoryId = category.Id };
                        _context.WebLinkCategories.Add(webLinkCategory);

                    }
                    _context.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(Guid id)
        {
            var webLink = _context.WebLinks.FirstOrDefault(c => c.Id == id);
            var newWebLinkViewModel = new WebLinkModifyViewModel { WebLink = webLink };
            newWebLinkViewModel.CategoryItems = _context.Categories.OrderBy(c => c.Name).Select(c => new CategoryItem { Id = c.Id, Name = c.Name, IsSelected = c.WebLinkCategories.Any(wc => wc.WebLinkId == id) }).ToList();


            if (webLink != null)
            {
                return View(newWebLinkViewModel);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, [FromForm] WebLink webLink)
        {
            try
            {
                var originalWebLInk = _context.WebLinks.FirstOrDefault(c => c.Id == id);
                _context.WebLinks.Remove(originalWebLInk);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<byte[]> LoadFavIcon(string url)
        {
            var httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync($"http://www.google.com/s2/favicons?domain={url}");
            byte[] content = await response.Content.ReadAsByteArrayAsync();
            return content;
        }
    }
}

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

            if (webLink != null)
            {
                return View(webLink);
            }
            return View("Index");
        }

        public ActionResult Create()
        {
            var newWebLink = new WebLink { Id = Guid.NewGuid(), CreatedDateTime = DateTime.Now };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] WebLink webLink)
        {
            try
            {
                webLink.Id = Guid.NewGuid();
                webLink.CreatedDateTime = DateTime.Now;

                _context.WebLinks.Add(webLink);
                _context.SaveChanges();

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

            if (webLink != null)
            {
                return View(webLink);
            }
            return View("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [FromForm] WebLink webLink)
        {
            try
            {
                var originalWebLink = _context.WebLinks.FirstOrDefault(c => c.Id == id);
                originalWebLink.Name = webLink.Name;
                originalWebLink.IsFaviourite = webLink.IsFaviourite;
                originalWebLink.Url = webLink.Url;
                _context.SaveChanges();

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

            if (webLink != null)
            {
                return View(webLink);
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
    }
}

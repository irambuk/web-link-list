using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebLinkList.EF;
using WebLinkList.Common;
using Microsoft.Extensions.Options;
using WebLinkList.EF.Model;

namespace WebLinkList.WebMvc.Controllers
{
    public class CategoryController : Controller
    {
        private WebLinkContext _context;
        private ConfigSettings _configSettings;

        public CategoryController(WebLinkContext context, IOptions<ConfigSettings> configSettingsOptions)
        {
            _context = context;
            _configSettings = configSettingsOptions.Value;
        }

        public ActionResult Index()
        {
            var categories = _context.Categories.OrderByDescending(c => c.CreatedDateTime).ToList();
            return View(categories);
        }

        public ActionResult Details(Guid id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                return View(category);
            }
            return View("Index");
        }

        public ActionResult Create()
        {
            var newCategory = new Category {Id = Guid.NewGuid(), CreatedDateTime = DateTime.Now };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Category category)
        {
            try
            {
                category.Id = Guid.NewGuid();
                category.CreatedDateTime = DateTime.Now;

                _context.Categories.Add(category);
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
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                return View(category);
            }
            return View("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, [FromForm] Category category)
        {
            try
            {
                var originalCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
                originalCategory.Name = category.Name;
                originalCategory.GraphColorInt = category.GraphColorInt;
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
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category != null)
            {
                return View(category);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Guid id, [FromForm] Category category)
        {
            try
            {
                var originalCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
                _context.Categories.Remove(originalCategory);
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
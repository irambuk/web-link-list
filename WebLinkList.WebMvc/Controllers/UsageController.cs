﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebLinkList.WebMvc.Controllers
{
    public class UsageController : Controller
    {
        // GET: Usage
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usage/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Usage/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: Usage/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usage/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
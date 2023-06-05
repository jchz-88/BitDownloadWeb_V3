﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitDownloadWeb_V3_1.Controllers
{
    [Authorize]
    public class FilesController : Controller
    {
        // GET: FilesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FilesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FilesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FilesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FilesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: FilesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FilesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

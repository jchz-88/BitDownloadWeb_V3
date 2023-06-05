using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BitDownloadWeb_V3_1.Controllers
{
    [Authorize]
    public class HostsController : Controller
    {
        // GET: HostsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HostsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HostsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HostsController/Create
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

        // GET: HostsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HostsController/Edit/5
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

        // GET: HostsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HostsController/Delete/5
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

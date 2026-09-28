using LTWLesson07.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTWLesson07.Controllers
{
    public class PmaMemberController : Controller
    {
        public static List<PmaMember> pmaMembers = new List<PmaMember>();
        // GET: PmaMemberController
        public ActionResult PmaIndex()
        {
            return View(pmaMembers);
        }

        // GET: PmaMemberController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PmaMemberController/Create
        public ActionResult PmaCreate()
        {
            return View();
        }

        // POST: PmaMemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PmaCreate(PmaMember pmamember)
        {
            if (!ModelState.IsValid)
            {
                return View(pmamember);
            }
            pmamember.PmaMemberId = pmaMembers.Count + 1;
            pmaMembers.Add(pmamember);
            return RedirectToAction("PmaIndex");
        }

        // GET: PmaMemberController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PmaMemberController/Edit/5
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

        // GET: PmaMemberController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PmaMemberController/Delete/5
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerMVC.Models;

namespace FortuneTellerMVC.Controllers
{
    public class BirthMonthsController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();

        // GET: BirthMonths
        public ActionResult Index()
        {
            return View(db.BirthMonths.ToList());
        }

        // GET: BirthMonths/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BirthMonth birthMonth = db.BirthMonths.Find(id);
            if (birthMonth == null)
            {
                return HttpNotFound();
            }
            return View(birthMonth);
        }

        // GET: BirthMonths/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BirthMonths/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BirthMonthID,BirthMonthName")] BirthMonth birthMonth)
        {
            if (ModelState.IsValid)
            {
                db.BirthMonths.Add(birthMonth);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(birthMonth);
        }

        // GET: BirthMonths/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BirthMonth birthMonth = db.BirthMonths.Find(id);
            if (birthMonth == null)
            {
                return HttpNotFound();
            }
            return View(birthMonth);
        }

        // POST: BirthMonths/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BirthMonthID,BirthMonthName")] BirthMonth birthMonth)
        {
            if (ModelState.IsValid)
            {
                db.Entry(birthMonth).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(birthMonth);
        }

        // GET: BirthMonths/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BirthMonth birthMonth = db.BirthMonths.Find(id);
            if (birthMonth == null)
            {
                return HttpNotFound();
            }
            return View(birthMonth);
        }

        // POST: BirthMonths/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BirthMonth birthMonth = db.BirthMonths.Find(id);
            db.BirthMonths.Remove(birthMonth);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

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
    public class CustomersController : Controller
    {
        private FortuneTellerMVCEntities db = new FortuneTellerMVCEntities();



        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.BirthMonth).Include(c => c.Color);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.FirstName = customer.FirstName;
            ViewBag.LastName = customer.LastName;

            //retirement age - even/odd
            if (customer.Age % 2 == 0)
            {
                ViewBag.RetirementAge = 10;
            }
            else
            {
                ViewBag.RetirementAge = 25;
            }

            //birth month - bank account balance
            if (customer.BirthMonthID >= 1 && customer.BirthMonthID <= 4)
            {
                ViewBag.BankAccountBalance = "$1,000,000";
            }
            else if (customer.BirthMonthID >= 5 && customer.BirthMonthID <= 8)
            {
                ViewBag.BankAccountBalance = "$100";
            }
            else if (customer.BirthMonthID >= 9 && customer.BirthMonthID <= 12)
            {
                ViewBag.BankAccountBalance = "$24,000,000";
            }
            else
            {
                ViewBag.BankAccountBalance = "$0";
            }

            //color - specific mode of transportation
            switch (customer.ColorID)
            {
                case 1:
                    ViewBag.ModeOfTransportation = "Ferrari";
                    break;
                case 2:
                    ViewBag.ModeOfTransportation = "Porsche";
                    break;
                case 3:
                    ViewBag.ModeOfTransportation = "Lamborghini";
                    break;
                case 4:
                    ViewBag.ModeOfTransportation = "Bugatti";
                    break;
                case 5:
                    ViewBag.ModeOfTransportation = "Bentley";
                    break;
                case 6:
                    ViewBag.ModeOfTransportation = "BMW";
                    break;
                case 7:
                    ViewBag.ModeOfTransportation = "Mercedes-Benz";
                    break;
                default:
                    break;
            }

            //number of siblings - 0, 1, 2, 3, 4+ - vacation home location
            switch (customer.NumberOfSiblings)
            {
                case 0:
                    ViewBag.VacationHome = "Mykonos";
                    break;
                case 1:
                    ViewBag.VacationHome = "Maui";
                    break;
                case 2:
                    ViewBag.VacationHome = "Milan";
                    break;
                case 3:
                    ViewBag.VacationHome = "The Maldives";
                    break;
                default:
                    ViewBag.VacationHome = "Detroit";
                    break;

            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName");
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,BirthMonthID,ColorID,NumberOfSiblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BirthMonthID = new SelectList(db.BirthMonths, "BirthMonthID", "BirthMonthName", customer.BirthMonthID);
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "ColorName", customer.ColorID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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

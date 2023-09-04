using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using examen_Web_2_12_2018.Models;

namespace examen_Web_2_12_2018.Controllers
{
    public class AvtoesController : Controller
    {
        private Model_taxi db = new Model_taxi();

        // GET: Avtoes
        public ActionResult Index()
        {
            // Начало дописанного мной.
            int quantity_orders = db.Orders.Count();
            ViewBag.Quantity_orders = quantity_orders;
            var sum_orders = db.Orders.Sum(o => o.Pay);
            ViewBag.Sum_orders = sum_orders;
            // Конец дописанного мной.
            return View(db.Avtos.ToList());
        }

        // GET: Avtoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avto avto = db.Avtos.Find(id);
            if (avto == null)
            {
                return HttpNotFound();
            }
            // Начало дописанного мной.
            var orders_accomplished = db.Orders.Where(o => o.Id == id && o.State_Order == State_order.accomplished);
            ViewBag.Orders_accomplished = orders_accomplished;
            // Конец дописанного мной.
            return View(avto);
        }

        // GET: Avtoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avtoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name_avto,Driver,Telephone_driver,state,Seats")] Avto avto)
        {
            if (ModelState.IsValid)
            {
                db.Avtos.Add(avto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(avto);
        }

        // GET: Avtoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avto avto = db.Avtos.Find(id);
            if (avto == null)
            {
                return HttpNotFound();
            }
            return View(avto);
        }

        // POST: Avtoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name_avto,Driver,Telephone_driver,state,Seats")] Avto avto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(avto);
        }

        // GET: Avtoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Avto avto = db.Avtos.Find(id);
            if (avto == null)
            {
                return HttpNotFound();
            }
            return View(avto);
        }

        // POST: Avtoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Avto avto = db.Avtos.Find(id);
            db.Avtos.Remove(avto);
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

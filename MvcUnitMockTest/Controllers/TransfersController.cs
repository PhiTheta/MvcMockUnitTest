﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcUnitMockTest.DataAccess;
using MvcUnitMockTest.Models;

namespace MvcUnitMockTest.Controllers
{
    public class TransfersController : Controller
    {
        private BankContext db = new BankContext();

        // GET: Transfers
        public ActionResult Index()
        {
            //return View(db.Transfers.ToList());
            return View();
        }

        // GET: Transfers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Deposit()
        {
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deposit([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Withdraw()
        {
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Withdraw([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult Transfer()
        {
            return View();
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Transfer([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Transfers.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(transfer);
        }

        // GET: Transfers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // POST: Transfers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transfer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transfer);
        }

        // GET: Transfers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transfer transfer = db.Transfers.Find(id);
            if (transfer == null)
            {
                return HttpNotFound();
            }
            return View(transfer);
        }

        // POST: Transfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transfer transfer = db.Transfers.Find(id);
            db.Transfers.Remove(transfer);
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

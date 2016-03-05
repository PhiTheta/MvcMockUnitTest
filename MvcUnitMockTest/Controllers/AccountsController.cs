using System;
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
    public class AccountsController : Controller
    {
        private IRepository repository;

        public AccountsController(IRepository repository)
        {
            this.repository = repository;
        }

        public AccountsController()
        {
            this.repository = new WorkingRepository();
        }

        // GET: Accounts
        public ActionResult Index()
        {
            var products = repository.GetAllAccounts();
            return View(products);//repository.GetAllAccounts.ToList());
        }

        // GET: Accounts/Details/5
        public ActionResult Details()
        {
            var products = repository.GetAllAccounts();
            return View(products);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Account> accountList = repository.GetAllAccounts();
            Account account = accountList.FirstOrDefault(s => s.Id == id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult EditPost([Bind(Include = "Id,Number,Name,Locked,Type,Sum")] Account account)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAccounts(account);
                return RedirectToAction("Index");
            }
            return View(account);
        }
    }
}

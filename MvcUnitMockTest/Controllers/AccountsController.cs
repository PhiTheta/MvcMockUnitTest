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
                repository.ModifyAccount(account);
                return RedirectToAction("Details");
            }
            return View();//account);
        }

        // GET: Accounts
        public ActionResult AccountDetails(int? id)
        {
            var Account = repository.GetAllAccounts().FirstOrDefault( s => s.Id == id);
            ViewBag.Account = Account;
            var transfers = repository.GetAllTransfers().Where(a => a.IdTo == Account.Id || a.IdFrom == Account.Id);
            return View(transfers);
        }

        // GET: Transfers/Create
        public ActionResult TransferAdd(int? id)
        {
            Transfer transfer = new Transfer();
            transfer.Time = DateTime.Now;
            transfer.IdTo = (int)id;
            transfer.IdFrom = (int)id;
            return View(transfer);
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("TransferAdd")]
        public ActionResult TransferAddPost([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                var Account = repository.GetAllAccounts().FirstOrDefault(s => s.Id == transfer.IdTo);
                ViewBag.Account = Account;
                transfer.IdFrom = 0;
                transfer.Time = DateTime.Now;
                //transfer.IdTo 
                var accountList = repository.GetAllAccounts();
                var toAc = accountList.FirstOrDefault(s => s.Id == transfer.IdTo);
                toAc.Sum += transfer.Sum;
                repository.AddTransfer(transfer);
                
                return RedirectToAction("Details", repository.GetAllAccounts().ToList());
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult TransferRemove(int? id)
        {
            Transfer transfer = new Transfer();
            transfer.Time = DateTime.Now;
            transfer.IdFrom = (int)id;
            transfer.IdFrom = 0;
            return View(transfer);
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("TransferRemove")]
        public ActionResult TransferRemovePost([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                var Account = repository.GetAllAccounts().FirstOrDefault(s => s.Id == transfer.IdTo);
                ViewBag.Account = Account;
                transfer.IdTo = 0;
                transfer.Time = DateTime.Now;
                //transfer.IdTo 
                var accountList = repository.GetAllAccounts();
                var toAc = accountList.FirstOrDefault(s => s.Id == transfer.IdFrom);
                toAc.Sum -= transfer.Sum;
                repository.AddTransfer(transfer);

                return RedirectToAction("Details", repository.GetAllAccounts().ToList());


                //repository.AddTransfer(transfer);
                //var accountList = repository.GetAllAccounts();
                //var toAc = accountList.FirstOrDefault(s => s.Id == transfer.IdFrom);
                //toAc.Sum -= transfer.Sum;
                //return RedirectToAction("AccountDetails");
            }

            return View(transfer);
        }

        // GET: Transfers/Create
        public ActionResult TransferMove(int? id1, int? id2)
        {
            Transfer transfer = new Transfer();
            transfer.Time = DateTime.Now;
            transfer.IdFrom = (int)id1;
            transfer.IdTo = (int)id2;
            return View(transfer);
        }

        // POST: Transfers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("TransferMove")]
        public ActionResult TransferMovePost([Bind(Include = "Id,IdFrom,IdTo,Sum,Time")] Transfer transfer)
        {
            if (ModelState.IsValid)
            {
                repository.AddTransfer(transfer);
                var accountList = repository.GetAllAccounts();
                var fromAc = accountList.FirstOrDefault(s => s.Id == transfer.IdTo);
                fromAc.Sum += transfer.Sum;
                var toAc = accountList.FirstOrDefault(s => s.Id == transfer.IdFrom);
                toAc.Sum -= transfer.Sum;
                return RedirectToAction("AccountDetails");
            }
            return View(transfer);
        }
    }
}

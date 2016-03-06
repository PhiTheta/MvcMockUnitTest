using MvcUnitMockTest.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcUnitMockTest.Models
{
    public class WorkingRepository : IRepository
    {
        private BankContext db;

        public WorkingRepository()
        {
            db = new BankContext();
        }

        public List<Account> GetAllAccounts()
        {
            return db.Accounts.ToList();        
        }

        public List<Transfer> GetAllTransfers()
        {
            return db.Transfers.ToList();
        }

        //public void SaveAccounts(Account account)
        //{
        //    db.Entry(account).State = EntityState.Modified;
        //}

        //public void SaveTransfers(Transfer transfer)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add(Transfer transfer)
        //{
        //    db.Transfers.Add(transfer);
        //    db.SaveChanges();
        //}

        public void AddTransfer(Transfer transfer)
        {
            db.Transfers.Add(transfer);
            db.SaveChanges();
        }

        public void ModifyAccount(Account account)
        {
            db.Entry(account).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}
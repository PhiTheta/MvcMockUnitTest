using MvcUnitMockTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcUnitMockTest.DataAccess
{
    public class BankContext : DbContext
    {
        public BankContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transfer> Transfers { get; set; }         
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUnitMockTest.Models
{
    public interface IRepository
    {
        List<Account> GetAllAccounts();
        List<Transfer> GetAllTransfers();

        void AddTransfer(Transfer transfer);
        void ModifyAccount(Account account);
    }
}

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

        void SaveAccounts(Account account);
        void SaveTransfers(Transfer transfer);
    }
}

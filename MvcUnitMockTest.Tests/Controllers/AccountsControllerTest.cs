using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Telerik.JustMock;
using MvcUnitMockTest.Models;
using System.Collections.Generic;
using MvcUnitMockTest.Controllers;
using System.Web.Mvc;
using System.Net;

namespace MvcUnitMockTest.Tests.Controllers
{
    [TestClass]
    public class AccountsControllerTest
    {
        [TestMethod]
        public void Index_Returns_All_Accounts_In_Db()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();
                    
            // Act
            AccountsController controller = new AccountsController(accountRepository);
            ViewResult viewResult = (ViewResult)controller.Index();
            var model = viewResult.Model as IEnumerable<Account>;

            // Assert
            Assert.AreEqual(4, model.Count());

        }

        [TestMethod]
        public void Details_Returns_All_Accounts_In_Db()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();
                    
            // Act
            AccountsController controller = new AccountsController(accountRepository);
            ViewResult viewResult = (ViewResult)controller.Details();
            var model = viewResult.Model as IEnumerable<Account>;

            // Assert
            Assert.AreEqual(4, model.Count());

        }

        [TestMethod]
        public void Edit_Get_Parameter_NULL()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(accountRepository);
            var viewResult = controller.Edit(null) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(viewResult, typeof(HttpStatusCodeResult));
            Assert.AreEqual((int)HttpStatusCode.BadRequest, viewResult.StatusCode);

        }

        [TestMethod]
        public void Edit_Get_Parameter_Not_Found()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(accountRepository);
            var viewResult = (ActionResult)controller.Edit(7);
            var viewObj = viewResult as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(viewObj);
            Assert.IsInstanceOfType(viewObj, typeof(HttpStatusCodeResult));
            Assert.AreEqual((int)HttpStatusCode.NotFound, viewObj.StatusCode);

        }

        [TestMethod]
        public void Edit_Get_Item_With_Id_2()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            Mock.Arrange(() => accountRepository.SaveAccounts(Arg.IsAny<Account>()))
                .DoInstead((Account account) =>
                {
                    List<Account> accountList = accountRepository.GetAllAccounts();
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        if (accountList[i] == account)
                        {
                            accountList[i].Locked = account.Locked;
                            accountList[i].Name = account.Name;
                            accountList[i].Sum = account.Sum;
                            accountList[i].Type = account.Type;
                        }
                    }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(accountRepository);
            ViewResult viewResult = (ViewResult)controller.Edit(2);
            var model = viewResult.Model as Account;

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual("Lönekonto", model.Name);
        }

        [TestMethod]
        public void Edit_Post()
        {
            // Arange
            var accountRepository = Mock.Create<IRepository>();
            Mock.Arrange(() => accountRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            Mock.Arrange(() => accountRepository.SaveAccounts(Arg.IsAny<Account>()))
                .DoInstead((Account account) =>
                {
                    List<Account> accountList = accountRepository.GetAllAccounts();
                    for (int i = 0; i < accountList.Count; i++)
                    {
                        if (accountList[i].Id == account.Id)
                        {
                            accountList[i].Locked = account.Locked;
                            accountList[i].Name = account.Name;
                            accountList[i].Sum = account.Sum;
                            accountList[i].Type = account.Type;
                        }
                    }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(accountRepository);
            Account account2 = new Account { Id = 2, Name = "Lönekonto 1", Number = "123456-2", Sum = 100.0, Type = "-", Locked = false };
            var viewResult = controller.EditPost(account2) as RedirectToRouteResult;
            
            // Assert
            Assert.IsNotNull(viewResult);
            Assert.IsFalse(viewResult.Permanent); // Or IsTrue if you use RedirectToActionPermanent
            Assert.AreEqual("Index", viewResult.RouteValues["Action"]);

            var Account2 = accountRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 2);
            Assert.AreEqual("Lönekonto 1", Account2.Name);
        }
    }
}

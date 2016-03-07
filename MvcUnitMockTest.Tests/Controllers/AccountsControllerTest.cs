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

            Mock.Arrange(() => accountRepository.ModifyAccount(Arg.IsAny<Account>()))
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

            Mock.Arrange(() => accountRepository.ModifyAccount(Arg.IsAny<Account>()))
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
            Assert.AreEqual("Details", viewResult.RouteValues["Action"]);

            var Account2 = accountRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 2);
            Assert.AreEqual("Lönekonto 1", Account2.Name);
        }

        // TODO CreateView
        [TestMethod]
        public void AccountDetails_Check_Account_Is_In_ViewBag()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();
            
            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()
                {
                    new Transfer {Id = 1, IdFrom=1, IdTo=2, Time=DateTime.Now, Sum=50.0 },
                    new Transfer {Id = 2, IdFrom=2, IdTo=3, Time=DateTime.Now, Sum=75.0 },
                    new Transfer {Id = 3, IdFrom=4, IdTo=2, Time=DateTime.Now, Sum=25.0 },
                    new Transfer {Id = 4, IdFrom=4, IdTo=1, Time=DateTime.Now, Sum=100.0 }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            ViewResult viewResult = (ViewResult)controller.AccountDetails(2);

            // Assert
            Assert.IsNotNull(viewResult.ViewBag.Account);
            Assert.AreEqual("Lönekonto", viewResult.ViewBag.Account.Name);

        }

        // TODO CreateView
        [TestMethod]
        public void AccountDetails_Returns_All_Transfers_In_Db_For_One_Account()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();

            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()
                {
                    new Transfer {Id = 1, IdFrom=1, IdTo=2, Time=DateTime.Now, Sum=50.0 },
                    new Transfer {Id = 2, IdFrom=2, IdTo=3, Time=DateTime.Now, Sum=75.0 },
                    new Transfer {Id = 3, IdFrom=4, IdTo=2, Time=DateTime.Now, Sum=25.0 },
                    new Transfer {Id = 4, IdFrom=4, IdTo=1, Time=DateTime.Now, Sum=100.0 }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            ViewResult viewResult = (ViewResult)controller.AccountDetails(1);
            var model = viewResult.Model as IEnumerable<Transfer>;

            // Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
            Assert.AreEqual(2, model.ToList()[0].IdTo);
            Assert.AreEqual(1, model.ToList()[0].IdFrom);
            Assert.AreEqual(1, model.ToList()[1].IdTo);
            Assert.AreEqual(4, model.ToList()[1].IdFrom);

        }

        // TODO CreateView
        [TestMethod]
        public void AccountDetails_Check_That_Account_Is_In_ViewBag()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();
            
            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=0.0, Type="-", Locked=false},
                    new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=0.0, Type="-", Locked=false},
                    new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=0.0, Type="-", Locked=false},
                    new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=0.0, Type="-", Locked=false}
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()
                {
                    new Transfer {Id = 1, IdFrom=1, IdTo=2, Time=DateTime.Now, Sum=50.0 },
                    new Transfer {Id = 2, IdFrom=2, IdTo=3, Time=DateTime.Now, Sum=75.0 },
                    new Transfer {Id = 3, IdFrom=4, IdTo=2, Time=DateTime.Now, Sum=25.0 },
                    new Transfer {Id = 4, IdFrom=4, IdTo=1, Time=DateTime.Now, Sum=100.0 }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            ViewResult viewResult = (ViewResult)controller.AccountDetails(4);

            // Assert
            Assert.IsNotNull(viewResult.ViewBag.Account);
            Assert.AreEqual("Sparkonto 2", viewResult.ViewBag.Account.Name);

        }

        // TODO CreateView
        [TestMethod]
        public void AccountDetails_Check_That_AccountSum_Is_Same_As_TransferSum()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();

            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account { Id = 1, Number = "123456-1", Name = "Betalkonto", Sum = 350.0, Type = "-", Locked = false },
                    new Account { Id = 2, Number = "123456-2", Name = "Lönekonto", Sum = 450.0, Type = "-", Locked = false },
                    new Account { Id = 3, Number = "123456-3", Name = "Sparkonto 1", Sum = 575.0, Type = "-", Locked = false },
                    new Account { Id = 4, Number = "123456-4", Name = "Sparkonto 2", Sum = 625.0, Type = "-", Locked = false }
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()
                {
                    new Transfer { Id = 1, IdFrom = 0, IdTo = 1, Sum = 500.0, Time = DateTime.Now },
                    new Transfer { Id = 2, IdFrom = 0, IdTo = 2, Sum = 500.0, Time = DateTime.Now },
                    new Transfer { Id = 3, IdFrom = 0, IdTo = 3, Sum = 500.0, Time = DateTime.Now },
                    new Transfer { Id = 4, IdFrom = 0, IdTo = 4, Sum = 500.0, Time = DateTime.Now },
                    new Transfer { Id = 5, IdFrom = 2, IdTo = 1, Sum = 150.0, Time = DateTime.Now },
                    new Transfer { Id = 6, IdFrom = 3, IdTo = 2, Sum = 100.0, Time = DateTime.Now },
                    new Transfer { Id = 7, IdFrom = 1, IdTo = 3, Sum = 175.0, Time = DateTime.Now },
                    new Transfer { Id = 8, IdFrom = 1, IdTo = 4, Sum = 125.0, Time = DateTime.Now }
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            ViewResult viewResult = (ViewResult)controller.AccountDetails(2);
            var model = viewResult.Model as IEnumerable<Transfer>;
            var plusSum = model.ToList().Where(x => x.IdTo == 2).Sum(p => p.Sum);
            var minusSum = model.ToList().Where(x => x.IdFrom == 2).Sum(p => p.Sum); 

            // Assert
            Assert.IsNotNull(viewResult.ViewBag.Account);
            Assert.IsNotNull(model);
            Assert.AreEqual(viewResult.ViewBag.Account.Sum, plusSum - minusSum);

        }

		[TestMethod]
		public void TransferAddGet_Item_With_Id_2()
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
			ViewResult viewResult = (ViewResult)controller.TransferAdd(2);
			var model = viewResult.Model as Transfer;

			// Assert
			Assert.IsNotNull(model);
			Assert.AreEqual(2, model.IdTo);
		}

        // TODO CreateView
        [TestMethod]
		public void TransferAddPost_Add_Money_To_Account()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();

            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account { Id = 1, Number = "123456-1", Name = "Betalkonto", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 2, Number = "123456-2", Name = "Lönekonto", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 3, Number = "123456-3", Name = "Sparkonto 1", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 4, Number = "123456-4", Name = "Sparkonto 2", Sum = 0.0, Type = "-", Locked = false }
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()).MustBeCalled();

            Mock.Arrange(() => transfersRepository.AddTransfer(Arg.IsAny<Transfer>()))

                .DoInstead((Transfer transfer) =>
                {
                    List<Transfer> transferList = transfersRepository.GetAllTransfers();
                    transferList.Add(transfer);
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            Transfer transfer1 = new Transfer {Id=1, IdFrom=0, IdTo=2, Time=DateTime.Now,Sum=1000.0};
            RedirectToRouteResult actionResult = (RedirectToRouteResult)controller.TransferAddPost(transfer1);
            var Account2 = transfersRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 2);

            // Assert
            Assert.IsFalse(actionResult.Permanent);
            Assert.AreEqual("Details", actionResult.RouteValues["Action"]);
            Assert.AreEqual(1000.0, Account2.Sum);
        }

		[TestMethod]
		public void TransferRemoveGet_Item_With_Id_2()
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
			ViewResult viewResult = (ViewResult)controller.TransferRemove(2);
			var model = viewResult.Model as Transfer;

			// Assert
			Assert.IsNotNull(model);
			Assert.AreEqual(2, model.IdFrom);
		}

		// TODO CreateView
        [TestMethod]
		public void TransferRemovePost_Remove_Money_From_Account()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();

            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account { Id = 1, Number = "123456-1", Name = "Betalkonto", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 2, Number = "123456-2", Name = "Lönekonto", Sum = 500.0, Type = "-", Locked = false },
                    new Account { Id = 3, Number = "123456-3", Name = "Sparkonto 1", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 4, Number = "123456-4", Name = "Sparkonto 2", Sum = 0.0, Type = "-", Locked = false }
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()).MustBeCalled();

            Mock.Arrange(() => transfersRepository.AddTransfer(Arg.IsAny<Transfer>()))

                .DoInstead((Transfer transfer) =>
                {
                    List<Transfer> transferList = transfersRepository.GetAllTransfers();
                    transferList.Add(transfer);
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            Transfer transfer1 = new Transfer { Id = 1, IdFrom = 2, IdTo = 0, Time = DateTime.Now, Sum = 100.0 };
            RedirectToRouteResult actionResult = (RedirectToRouteResult)controller.TransferRemovePost(transfer1);
            var Account2 = transfersRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 2);

            // Assert
            Assert.IsFalse(actionResult.Permanent);
            Assert.AreEqual("Details", actionResult.RouteValues["Action"]);
            Assert.AreEqual(400.0, Account2.Sum);
        }

		[TestMethod]
		public void TransferMoveGet_Item_With_Id_2()
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
			ViewResult viewResult = (ViewResult)controller.TransferMove(2);
			var model = viewResult.Model as Transfer;

			// Assert
			Assert.IsNotNull(model);
			Assert.AreEqual(2, model.IdFrom);
		}

        // TODO CreateView
        [TestMethod]
		public void TransferMovePost_Move_Money_From_Account2_To_Account1()
        {
            // Arange
            var transfersRepository = Mock.Create<IRepository>();

            Mock.Arrange(() => transfersRepository.GetAllAccounts()).
                Returns(new List<Account>()
                {
                    new Account { Id = 1, Number = "123456-1", Name = "Betalkonto", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 2, Number = "123456-2", Name = "Lönekonto", Sum = 500.0, Type = "-", Locked = false },
                    new Account { Id = 3, Number = "123456-3", Name = "Sparkonto 1", Sum = 0.0, Type = "-", Locked = false },
                    new Account { Id = 4, Number = "123456-4", Name = "Sparkonto 2", Sum = 0.0, Type = "-", Locked = false }
                }).MustBeCalled();

            Mock.Arrange(() => transfersRepository.GetAllTransfers()).
                Returns(new List<Transfer>()).MustBeCalled();

            Mock.Arrange(() => transfersRepository.AddTransfer(Arg.IsAny<Transfer>()))

                .DoInstead((Transfer transfer) =>
                {
                    List<Transfer> transferList = transfersRepository.GetAllTransfers();
                    transferList.Add(transfer);
                }).MustBeCalled();

            // Act
            AccountsController controller = new AccountsController(transfersRepository);
            Transfer transfer1 = new Transfer { Id = 1, IdFrom = 2, IdTo = 1, Time = DateTime.Now, Sum = 100.0 };
            RedirectToRouteResult actionResult = (RedirectToRouteResult)controller.TransferMovePost(transfer1);
            var Account1 = transfersRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 1);
            var Account2 = transfersRepository.GetAllAccounts().ToList().FirstOrDefault(s => s.Id == 2);
            
            // Assert
            Assert.IsFalse(actionResult.Permanent);
            Assert.AreEqual("Details", actionResult.RouteValues["Action"]);
            Assert.AreEqual(400.0, Account2.Sum);
            Assert.AreEqual(100.0, Account1.Sum);
        }
    }
}

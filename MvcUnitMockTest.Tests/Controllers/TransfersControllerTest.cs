using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcUnitMockTest;
using MvcUnitMockTest.Controllers;
using Telerik.JustMock;
using MvcUnitMockTest.Models;

namespace MvcUnitMockTest.Tests.Controllers
{
    [TestClass]
    public class TransfersControllerTest
    {
        //[TestMethod]
        //public void Index_Returns_All_Transferss_In_Db()
        //{
        //    // Arange
        //    var transfersRepository = Mock.Create<IRepository>();
        //    Mock.Arrange(() => transfersRepository.GetAllTransfers()).
        //        Returns(new List<Transfer>()
        //        {
        //            new Transfer {Id = 1, IdFrom=1, IdTo=2, Time=DateTime.Now, Sum=50.0 },
        //            new Transfer {Id = 2, IdFrom=2, IdTo=3, Time=DateTime.Now, Sum=75.0 },
        //            new Transfer {Id = 3, IdFrom=4, IdTo=2, Time=DateTime.Now, Sum=25.0 },
        //            new Transfer {Id = 4, IdFrom=4, IdTo=1, Time=DateTime.Now, Sum=100.0 }
        //        }).MustBeCalled();

        //    //new Account {Id = 1, Name="Beatlkonto", Number="123456-1", Sum=100.0, Type="-", Locked=false},
        //    //new Account {Id = 2, Name="Lönekonto", Number="123456-2", Sum=100.0, Type="-", Locked=false},
        //    //new Account {Id = 3, Name="Sparkonto 1", Number="123456-3", Sum=100.0, Type="-", Locked=false},
        //    //new Account {Id = 4, Name="Sparkonto 2", Number="123456-4", Sum=100.0, Type="-", Locked=false}

        //    // Act
        //    TransfersController controller = new TransfersController(transfersRepository);
        //    ViewResult viewResult = (ViewResult)controller.Index();
        //    var model = viewResult.Model as IEnumerable<Account>;

        //    // Assert
        //    Assert.AreEqual(4, model.Count());
        //}


        //[TestMethod]
        //public void Index()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Index() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void About()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.About() as ViewResult;

        //    // Assert
        //    Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        //}

        //[TestMethod]
        //public void Contact()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.Contact() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}
    }
}

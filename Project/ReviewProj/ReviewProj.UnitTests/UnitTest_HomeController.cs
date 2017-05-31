using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ReviewProj.Domain.Abstract;
using ReviewProj.Domain.Concrete;
using ReviewProj.Domain.Entities;
using System.Linq;
using ReviewProj.WebUI.Controllers;
using System.Collections.Generic;
using ReviewProj.WebUI.Models;

namespace ReviewProj.UnitTests
{
    [TestClass]
    public class UnitTest_HomeController
     {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IEnterpriseRepository> mock = new Mock<IEnterpriseRepository>();
            mock.Setup(m => m.Enterprises).Returns(new Enterprise []
                {
                    new Enterprise { EntId = 1, Name = "E1" },
                    new Enterprise { EntId = 2, Name = "E2" },
                    new Enterprise { EntId = 3, Name = "E3" },
                    new Enterprise { EntId = 4, Name = "E4" },
                    new Enterprise { EntId = 5, Name = "E5" },
                    new Enterprise { EntId = 6, Name = "E6" },
                }.AsQueryable());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 4;

            //EnterpriseListViewModel
            
            //IEnumerable<Enterprise> result = (IEnumerable<Enterprise>)controller.Index(3).Model;
        }
    }
}

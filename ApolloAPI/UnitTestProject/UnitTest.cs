using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ApolloAPI.Controllers.Client;
using System.Security.Principal;
using ApolloAPI.Data.Client.Item;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void GetAllDoctors_ShouldReturnAtLeastADoctor()
        {
            DoctorController controller = new DoctorController();
            controller.User = new GenericPrincipal(new GenericIdentity("bryantylai"), new string[] { "User" });
            IEnumerable<DoctorItem> doctorItems = controller.GetListOfDoctors();

            Assert.AreNotEqual(0, doctorItems.Count());
        }

        //[TestMethod]
        //public void GetAllDoctors_ShouldReturnAtLeastADoctor()
        //{
        //    DoctorController controller = new DoctorController();
        //    controller.User = new GenericPrincipal(new GenericIdentity("bryantylai"), new string[] { "User" });
        //    IEnumerable<DoctorItem> doctorItems = controller.GetListOfDoctors();

        //    Assert.AreNotEqual(0, doctorItems.Count());
        //}
    }
}

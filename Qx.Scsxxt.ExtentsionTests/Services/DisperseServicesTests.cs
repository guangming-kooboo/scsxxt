using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qx.Scsxxt.Extentsion.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qx.Scsxxt.Extentsion.Services.Tests
{
    [TestClass()]
    public class DisperseServicesTests
    {
        [TestMethod()]
        public void OneKeyToPracticeTest()
        {
            var services = new DisperseServices();
            //var result = services.OneKeyToPractice("14093");
            //Assert.IsTrue(result);
            var rollBackResult = services.OneKeyToPractice_RoleBack("1137325299");
            Assert.IsTrue(rollBackResult);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcWebApi.Controllers;
using MvcWebApi.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

namespace MvcWebApi.Test
{
    /// <summary>
    /// Summary description for FactorialTest
    /// </summary>
    [TestClass]
    public class FactorialTest
    {
        Mock<IFactorialService> _mockService;
        ValuesController _controller;
        public FactorialTest()
        {
            _mockService = new Mock<IFactorialService>();
            _controller = new ValuesController(_mockService.Object);
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestFactorial()
        {
            _mockService.Setup(x => x.MultiplyNumbers(4, 3)).Returns(12);
            _mockService.Setup(x => x.MultiplyNumbers(12, 2)).Returns(24);
            _mockService.Setup(x => x.MultiplyNumbers(24, 1)).Returns(24);

            var result = _controller.GetFactorial(4);

            _mockService.Verify(x => x.MultiplyNumbers(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(3));
            Assert.AreEqual(24, result);
        }

        [TestMethod]
        public void TestFactorialReal()
        {
            var realController = new ValuesController(new FactorialService());
            var result = realController.GetFactorial(4);

            Assert.AreEqual(24, result);
        }

        [TestMethod]
        public void TestNoContent()
        {
            var realController = new ValuesController(new FactorialService());
            var result = (StatusCodeResult) realController.GetEmpty();

            Assert.AreEqual(System.Net.HttpStatusCode.NoContent, result.StatusCode);
        }
    }
}

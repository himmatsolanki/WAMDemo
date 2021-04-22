using Moq;
using MvcWebApi.Controllers;
using MvcWebApi.Services;
using NUnit.Framework;
using System;

namespace MvcWebApi.Test
{
    //[TestFixture]
    public class TestProjectWithNunit
    {
        Mock<IFactorialService> _mockService;
        ValuesController _controller;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IFactorialService>(MockBehavior.Strict);
            _controller = new ValuesController(_mockService.Object);
        }

        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(0)]
        public void TestFactorial(int input)
        {
            int result = input;

            if (input > 0)
            {
                int number = input;
                do
                {
                    _mockService.Setup(x => x.MultiplyNumbers(result, number - 1)).Returns(result * (number - 1));
                    result = result * (number - 1);
                    number--;
                } while (number > 1);
            }


            var resultActual = _controller.GetFactorial(input);

            int times = input > 0 ? input - 1 : 0;
            _mockService.Verify(x => x.MultiplyNumbers(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(times));
            Assert.AreEqual(result, resultActual);
        }

        [TearDown]
        public void TearDown()
        {
            _mockService.VerifyAll();
        }
    }
}

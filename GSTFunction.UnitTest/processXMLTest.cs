using System;
using Xunit;
using GSTFunction;
using GSTFunction.Model;

namespace GSTFunction.UnitTest
{
    public class processXMLTest
    {
        private readonly XMLProcessService _service;

        public processXMLTest()
        {
            _service = new XMLProcessService();
        }

        [Fact]
        public void processXMLTest_HappyPath()
        {
            // Arrange
            var dataToProcessValue = "<expense>" +
                "<cost_centre>DEV002</cost_centre> " +
                "<total>1024.01</total> " +
                "<payment_method>personal card</payment_method> " +
                "</expense> ";

            var rightValue = new expense();
            rightValue.cost_centre = "DEV002";
            rightValue.total = "1024.01";
            rightValue.cost_centre = "personal card";
            rightValue.totalGSTexclusionamount = 102.401;
            rightValue.totalGSTamount = 921.609;

            // Action
            var resultValue = _service.convertXMLtoData(dataToProcessValue, .10);

            // Assert
            Assert.Equal(rightValue, rightValue);

        }

        [Fact]
        public void processXMLTest_MissingCostCenterUnHappyPath()
        {
            // Arrange
            var dataToProcessValue = "<expense>" +
                "<total>1024.01</total> " +
                "<payment_method>personal card</payment_method> " +
                "</expense> ";

            var rightValue = new expense();
            rightValue.cost_centre = "Unknown";
            rightValue.total = "1024.01";
            rightValue.cost_centre = "personal card";
            rightValue.totalGSTexclusionamount = 102.401;
            rightValue.totalGSTamount = 921.609;

            // Action
            var resultValue = _service.convertXMLtoData(dataToProcessValue, .10);

            // Assert
            Assert.Equal(rightValue, rightValue);

        }

        [Fact]
        public void processXMLTest_MissingTotalUnHappyPath()
        {
            // Arrange
            var dataToProcessValue = "<expense>" +
                "<payment_method>personal card</payment_method> " +
                "</expense> ";

            var rightValue = new expense();
            rightValue.cost_centre = "DEV002";
            rightValue.total = "1024.01";
            rightValue.cost_centre = "personal card";
            rightValue.totalGSTexclusionamount = 0;
            rightValue.totalGSTamount = 0;

            
            // Action
            // Assert
            Assert.Throws<InvalidOperationException>(() => _service.convertXMLtoData(dataToProcessValue, .10));
        }
    }
}

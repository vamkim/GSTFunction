using System;
using Xunit;
using GSTFunction;

namespace GSTFunction.UnitTest
{
    public class extractXMLTest
    {
        private readonly XMLProcessService _service;

        public extractXMLTest()
        {
            _service = new XMLProcessService();
        }

        [Fact]
        public void extractXMLTest_HappyPath()
        {
            // Arrange
            var BodyEmail =
                "Hi Yvaine, Please create an expense claim for the below. Relevant details are marked up as requested… " +
                "<expense>" +
                "<cost_centre>DEV002</cost_centre> " +
                "<total>1024.01</total> " +
                "<payment_method>personal card</payment_method> " +
                "</expense> " +
                "From: Ivan Castle Sent: Friday, 16 February 2018 10:32 AM" +
                "To: Antoine Lloyd <Antoine.Lloyd@example.com> " +
                "Subject: test" +
                "Hi Antoine," +
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our " +
                "< description>development team’s project end celebration dinner</description> on <date>Tuesday " +
                "27 April 2017</date>. We expect to arrive around 7.15pm. Approximately 12 people but I’ll " +
                "confirm exact numbers closer to the day. " +
                "Regards," +
                "Ivan ";

            var rightValue = "<expense>" +
                "<cost_centre>DEV002</cost_centre> " +
                "<total>1024.01</total> " +
                "<payment_method>personal card</payment_method> " +
                "</expense> ";

            // Action
            var resultValue = _service.extractXMLinText(BodyEmail);

            // Assert
            Assert.Equal(rightValue, rightValue);

        }

        [Fact]
        public void extractXMLTest_UnHappyPath()
        {
            // Arrange
            var BodyEmail =
                "Hi Yvaine, Please create an expense claim for the below. Relevant details are marked up as requested… " +
                "<expense>" +
                "<cost_centre>DEV002</cost_centre> " +
                "<total>1024.01</total> " +
                "<payment_method>personal card</payment_method> " +
                "From: Ivan Castle Sent: Friday, 16 February 2018 10:32 AM" +
                "To: Antoine Lloyd <Antoine.Lloyd@example.com> " +
                "Subject: test" +
                "Hi Antoine," +
                "Please create a reservation at the <vendor>Viaduct Steakhouse</vendor> our " +
                "< description>development team’s project end celebration dinner</description> on <date>Tuesday " +
                "27 April 2017</date>. We expect to arrive around 7.15pm. Approximately 12 people but I’ll " +
                "confirm exact numbers closer to the day. " +
                "Regards," +
                "Ivan ";

            // Action
            // Assert
            Assert.Throws<InvalidOperationException>(() => _service.extractXMLinText(BodyEmail));

        }
    }
}

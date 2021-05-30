using System;
using Xunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GSTFunction;
using GSTFunction.Model;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace GSTFunction.UnitTest
{
    public class functionTest
    {
        
        [Fact]
        public void functionSubCallTest_HappyPath()
        {
            //Arrange
            var configbuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("local.settings.json", false, true);

            var config = configbuilder.Build();
            var GSTPersentage = config["GSTpercentage"];

            // Start-up Code for Injection
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<IXmlProcessService, XMLProcessService>();
            services.AddSingleton<GSTFunction, GSTFunction>();


            var serviceProvider = services.BuildServiceProvider();

            var GSTFunctionSubCall = (GSTFunction)serviceProvider.GetService(typeof(GSTFunction));

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

            var rightValue = new expense();
            rightValue.cost_centre = "DEV002";
            rightValue.total = "1024.01";
            rightValue.cost_centre = "personal card";
            rightValue.totalGSTexclusionamount = 102.401;
            rightValue.totalGSTamount = 921.609;

            // Action
            var resultValue = GSTFunctionSubCall.processGMTvalues(BodyEmail);


            // Assert
            Assert.Equal(rightValue, rightValue);

        }

    }

}

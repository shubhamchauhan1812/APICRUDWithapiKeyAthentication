using APICRUDWithapiKeyAthentication.Controllers;
using APICRUDWithapiKeyAthentication.Models;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICRUDWithapiKeyAthentication.Tests.Controllers
{
    public class CustomerControllerTest
    {
        [Fact]
        public void CustomerController_GetAllCustomers_ValidResult()
        {
            ///AAA
            //Arrange-
            
            var options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseInMemoryDatabase(databaseName: "coreApiApplication")
            .Options;
            var configuration = new Mock<IConfiguration>().Object;
            var logger = new Mock<ILogger<ApiKeyService>>().Object;

            //var apiKeyService = new ApiKeyService(configuration, logger);

            //CustomerController controller = new CustomerController(context, apiKeyService);
            string expectedResult = "Good day";

            //Act-
            //string result = controller.GetData();
            ////Assert-
            //Assert.Equal(expectedResult, result);
        }
    }
}

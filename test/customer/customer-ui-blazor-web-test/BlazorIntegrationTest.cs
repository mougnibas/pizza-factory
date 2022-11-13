// Copyright(c) 2022 Yoann MOUGNIBAS
// 
// This file is part of PizzaFactory.
// 
// PizzaFactory is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// PizzaFactory is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with PizzaFactory.  If not, see <https://www.gnu.org/licenses/>.


using Bunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Mougnibas.PizzaFactory.Customer.Business;
using Mougnibas.PizzaFactory.Customer.Contract;
using System.Net;

namespace Mougnibas.PizzaFactory.Customer.Ui.Blazor.Web.Test
{
    /// <summary>
    /// See https://codeburst.io/integration-tests-for-asp-net-core-web-apis-using-mstest-f4e222a3bc8a.
    /// </summary>
    [TestClass]
    public class BlazorIntegrationTest
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(Microsoft.VisualStudio.TestTools.UnitTesting.TestContext testContext)
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Inject a standalone business service to bypass the microservice dependency.
                    // An end to end approch is needed to effectively test all the components.
                    // The workflow is supposed to be :
                    // ui --> business-connector --> microservice --> actual business logic code.
                    // With this workaround, this is simplified to :
                    // ui ------------------------------------------> actual business logic code.
                    services.AddSingleton<IService, ServiceCore>();
                });
            });
        }

        [TestMethod]
        public async Task ShouldReturnSuccessResponseOnRoot()
        {
            // Arrange
            var client = _factory.CreateDefaultClient();
            var expected = HttpStatusCode.OK;

            // Act
            var response = await client.GetAsync("/");
            var actual = response.StatusCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ShouldReturnSuccessResponseOnPathUiPizza()
        {
            // Arrange
            var client = _factory.CreateDefaultClient();
            var expected = HttpStatusCode.OK;

            // Act
            var response = await client.GetAsync("/ui/pizza");
            var actual = response.StatusCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldReturnThisExactRawMarkup()
        {
            // Arrange
            // We need to inject AGAIN the service because of bunit.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<IService, ServiceCore>();
            var expected = @"<h1>Hello, world!</h1>
<h2>Pizza list :</h2>
<ul><li>My first pizza</li><li>My second pizza</li></ul>";

            // Act
            var componentUnderTest = context.RenderComponent<Pages.Index>();
            var actual = componentUnderTest.Markup;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldReturnThisH1()
        {
            // Arrange
            // We need to inject AGAIN the service because of bunit.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<IService, ServiceCore>();
            var expected = @"
<h1>Hello, world!</h1>";

            // Act
            var componentUnderTest = context.RenderComponent<Pages.Index>();
            var actual = componentUnderTest.Find("h1").ToMarkup();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ShouldReturnThisUl()
        {
            // Arrange
            // We need to inject AGAIN the service because of bunit.
            using var context = new Bunit.TestContext();
            context.Services.AddSingleton<IService, ServiceCore>();
            var expected = @"
<ul>
  <li>My first pizza</li>
  <li>My second pizza</li>
</ul>";

            // Act
            var componentUnderTest = context.RenderComponent<Pages.Index>();
            var actual = componentUnderTest.Find("ul").ToMarkup();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
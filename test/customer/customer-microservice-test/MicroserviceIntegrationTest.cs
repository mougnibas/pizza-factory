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


using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace Mougnibas.PizzaFactory.Customer.Microservice.Test
{
    /// <summary>
    /// See https://codeburst.io/integration-tests-for-asp-net-core-web-apis-using-mstest-f4e222a3bc8a.
    /// </summary>
    [TestClass]
    public class MicroserviceIntegrationTest
    {
        private static WebApplicationFactory<Program> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                // ... Configure test services
            });
        }

        [TestMethod]
        public async Task ShouldReturnSuccessResponse()
        {
            // Arrange
            var client = _factory.CreateDefaultClient();
            var expected = HttpStatusCode.OK;

            // Act
            var response = await client.GetAsync("api/pizza");
            var actual = response.StatusCode;

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task ShouldReturnThisTwoJsonPizza()
        {
            // Arrange
            var client = _factory.CreateDefaultClient();
            var expected = @"[
  {
    ""name"": ""My first pizza""
  },
  {
    ""name"": ""My second pizza""
  }
]";


            // Act
            var response = await client.GetAsync("api/pizza");
            var actual = await response.Content.ReadAsStringAsync();

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
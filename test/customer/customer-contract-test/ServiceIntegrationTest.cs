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

namespace Mougnibas.PizzaFactory.Customer.Contract.Test;

/// <summary>
/// See https://codeburst.io/integration-tests-for-asp-net-core-web-apis-using-mstest-f4e222a3bc8a.
/// </summary>
[TestClass]
public class ServiceIntegrationTest
{
    private static WebApplicationFactory<Program> _factory;

    [ClassInitialize]
#pragma warning disable IDE0060 // Remove unused parameter
    public static void ClassInit(TestContext testContext)
#pragma warning restore IDE0060 // Remove unused parameter
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            // ... Configure test services
        });
    }

    [TestMethod]
    public async Task TestDefaultGet()
    {
        // Arrange
        HttpClient httpClient = _factory.CreateDefaultClient();
        Service service = new Service(httpClient);
        Pizza[] expected =
        {
            new Pizza("My first pizza"),
            new Pizza("My second pizza")
        };

        // Act
        Pizza[] actual = await service.GetAsync();

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        _factory.Dispose();
    }
}
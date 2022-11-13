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

using Mougnibas.PizzaFactory.Customer.Contract;

namespace Mougnibas.PizzaFactory.Customer.Business.Test;

[TestClass]
public class ServiceImplUnitTest
{
    [TestMethod]
    public void TestDefaultGet()
    {
        // Arrange
        ServiceImpl service = new();
        Pizza[] expected =
        {
            new Pizza("My first pizza"),
            new Pizza("My second pizza")
        };

        // Act
        Pizza[] actual = service.Get();

        // Assert
        CollectionAssert.AreEqual(expected, actual);
    }
}
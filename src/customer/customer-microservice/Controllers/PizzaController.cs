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

using Microsoft.AspNetCore.Mvc;
using Mougnibas.PizzaFactory.Customer.Contract;

namespace Mougnibas.PizzaFactory.Customer.Microservice.Controllers;

[ApiController]
[Route("/api/pizza")]
public class PizzaController : ControllerBase
{

    private readonly ILogger<PizzaController> _logger;

    private readonly IService _service;

    public PizzaController(ILogger<PizzaController> logger, IService service)
    {
        _logger = logger;
        _service = service;
    }

    [HttpGet]
    public Pizza[] Get()
    {
        // Log some information
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Get is invoked");
        }

        return _service.GetPizza();
    }
}

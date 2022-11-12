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

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Hosting;
using Mougnibas.PizzaFactory.Customer.Business;
using Mougnibas.PizzaFactory.Customer.Contract;

namespace Mougnibas.PizzaFactory.Customer.Ui.Blazor.Web
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            // Add custom service (require an active microservice to be run)
            builder.Services.AddSingleton<IService, Service>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}

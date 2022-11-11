using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Mougnibas.PizzaFactory.Customer.Business;
using Mougnibas.PizzaFactory.Customer.Contract;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Add integrated service (not the microservice version)
builder.Services.AddSingleton<IService, ServiceImpl>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

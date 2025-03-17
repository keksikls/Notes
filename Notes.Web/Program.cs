using Notes.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notes.Data.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AdIdentity();
builder.Services.AddRazorPages();
builder.Services.AddConfigureCoockie();
builder.Services.AddCustomLogging();
builder.Services.AddControllersWithViews(); 
builder.Services.AddEndpointsApiExplorer();

builder.AddData();
builder.AddApplicationServices();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSerilogRequestLogging();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=User}/{controller=Landing}/{action=Index}/{id?}"
);

app.Run();
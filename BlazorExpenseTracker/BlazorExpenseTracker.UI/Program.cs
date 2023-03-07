using BlazorExpenseTracker.UI.Interfaces;
using BlazorExpenseTracker.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorExpenseTracker.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            //Agregamos AddCircuitOptions(options => options.DetailedErrors = true); nos permite ver los errores en el navegador
            builder.Services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);
            //Configuro la Uri de la API
            //applicationUrl: HTTP->:30000
            //Uri HTTPS: Port->44345
            builder.Services.AddHttpClient<ICategoryService, CategoryService>(
                client => { client.BaseAddress = new Uri("https://localhost:44345"); });
            builder.Services.AddHttpClient<IExpenseService, ExpenseService>(
               client => { client.BaseAddress = new Uri("https://localhost:44345"); });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
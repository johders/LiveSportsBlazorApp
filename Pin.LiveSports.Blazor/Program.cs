using Pin.LiveSports.Blazor.Data;
using Pin.LiveSports.Blazor.Hubs;
using Pin.LiveSports.Blazor.Constants;
using Pin.LiveSports.Core.Services;
using Pin.LiveSports.Core.Services.Interfaces;

namespace Pin.LiveSports.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddSingleton<IReportService, ReportService>();
            builder.Services.AddSignalR();

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
            app.MapHub<LiveReportHub>(AppConstants.HubUrl);

            app.Run();
        }
    }
}
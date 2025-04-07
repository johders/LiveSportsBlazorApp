using Pin.LiveSports.Blazor.Constants;
using Pin.LiveSports.Blazor.Hubs;
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

            builder.Services.AddSingleton<IReportService, ReportService>();
            builder.Services.AddSingleton<ICompetitionService, CompetitionService>();
            builder.Services.AddSignalR();

            var app = builder.Build();

            var imagesPath = Path.Combine(app.Environment.WebRootPath, "game-images");
            if (Directory.Exists(imagesPath))
            {
                Directory.Delete(imagesPath, true);
            }

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
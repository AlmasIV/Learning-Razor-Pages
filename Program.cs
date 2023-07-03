using Microsoft.AspNetCore.Authentication.Cookies;

using Learning_Razor_Pages.Services;
using Learning_Razor_Pages.Model.DefaultConnectionString;
using Learning_Razor_Pages.Services.UserAccess;
using Learning_Razor_Pages.Services.DepositAccess;
using Learning_Razor_Pages.Services.CookiesAuthentication;
using Learning_Razor_Pages.Services.HistoryOfOperations;

using Microsoft.AspNetCore.Authentication;

namespace Learning_Razor_Pages;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { 
            options.LoginPath = "/Identity/LogIn";
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
        });
        builder.Services.AddAuthorization();

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddSingleton<DefaultConnectionString>();

        builder.Services.AddScoped<IUserAccess, UserAccess>();

        builder.Services.AddScoped<IHistoryOfOperations, HistoryOfOperations>();

        builder.Services.AddScoped<ICookiesAuthentication, CookiesAuthentication>();

        builder.Services.AddScoped<IDepositAccess, DepositAccess>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        else {
            app.UseExceptionHandler("/Error");
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.Map("/clear", async (context) => {
            await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        });
        app.MapRazorPages();
        app.Run();
    }
}

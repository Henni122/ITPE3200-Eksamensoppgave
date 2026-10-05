using Microsoft.EntityFrameworkCore;
using StudyRoomBooking.Web.Data;
using StudyRoomBooking.Web.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRoomRepository, RoomRepository>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();


// The Serilog must happen BEFORE the var app = builder.Build();
//Register Serilog with Dependency Injection 
//Start Serilog
builder.Services.AddSerilog(config =>
{
    config
        .MinimumLevel.Information()
        .WriteTo.Console()
        .WriteTo.File($"Logs/app_{DateTime.Now:ddMMyyyy_HHmmss}.log");

});
//End Serilog

var app = builder.Build();

// -- ERROR HANDLING START -- //

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapDefaultControllerRoute();

app.Run();
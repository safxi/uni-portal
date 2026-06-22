using Microsoft.EntityFrameworkCore;
using HiSUP.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("HiSUP_DB");
builder.Services.AddDbContext<HiSUPContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IDatabaseRepository, DatabaseRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

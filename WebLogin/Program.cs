using WebLogin.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WebDBContext>(options =>
<<<<<<< HEAD

    options.UseMySql(
        builder.Configuration.GetConnectionString("CadenaSQL"),
        new MySqlServerVersion(new Version(8, 0, 34))
));
=======
    options.UseMySql(
        builder.Configuration.GetConnectionString("CadenaSQL"),
        new MySqlServerVersion(new Version(8, 0, 34))
    ));

>>>>>>> 32ca4f151389de8757854818dcdafe3ee1ff0c51

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

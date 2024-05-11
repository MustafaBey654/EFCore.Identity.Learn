using EFCore.Identity.Learn.Context;
using EFCore.Identity.Learn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false; // alfanumeric karekter olmasýn.
    options.Password.RequireDigit = false; // Sayi içermesin
    options.Password.RequireLowercase = false; //küçük harf içermesin
    options.Password.RequiredLength = 1;
    options.User.RequireUniqueEmail = true; // Email zorunlu 

    ///
    options.SignIn.RequireConfirmedEmail = true; // Email onayý zorunlu
    options.Lockout.MaxFailedAccessAttempts = 3; // Kaç baþarýsýz deneme yapýlýrsa kitlensin. Burada email adresi onaylý olmasý zorunludur.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30); // 30 saniye boyunca kitlensin

}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // Þifre unutma durumunda token üretme 

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

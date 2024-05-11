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
    options.Password.RequireNonAlphanumeric = false; // alfanumeric karekter olmas�n.
    options.Password.RequireDigit = false; // Sayi i�ermesin
    options.Password.RequireLowercase = false; //k���k harf i�ermesin
    options.Password.RequiredLength = 1;
    options.User.RequireUniqueEmail = true; // Email zorunlu 

    ///
    options.SignIn.RequireConfirmedEmail = true; // Email onay� zorunlu
    options.Lockout.MaxFailedAccessAttempts = 3; // Ka� ba�ar�s�z deneme yap�l�rsa kitlensin. Burada email adresi onayl� olmas� zorunludur.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30); // 30 saniye boyunca kitlensin

}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders(); // �ifre unutma durumunda token �retme 

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

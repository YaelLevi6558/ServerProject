
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Repositories.Gift;
using Server.Services.Gift;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Server.Repositories.Donor;
using Server.Services.Donor;
using Server.Repositories.Purchase;
using Server.Services.IPurchaseService;
using Server.Repositories.Winner;
using Server.Services.Winner;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  

builder.Services.AddDbContext<ChineseAuctionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);
builder.Services.AddScoped<IGiftRepository, GiftRepository>();
builder.Services.AddScoped<IGiftService, GiftService>();
builder.Services.AddScoped<IDonorRepository, DonorRepository>();
builder.Services.AddScoped<IDonorService, DonorService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<IWinnerRepository, WinnerRepository>();
builder.Services.AddScoped<IWinnerService, WinnerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ChineseAuction",
        Description = "A simple example ASP.NET Core API to manage books",
        Contact = new OpenApiContact
        {
            Name = "Yael levi",
            Email = "yael0583276558@gmail.com",
            Url = new Uri("https://yourwebsite.com"),
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChineseAuction API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();











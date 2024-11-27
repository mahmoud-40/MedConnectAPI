using Medical.Models;
using Medical.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddDbContext<MedicalContext>(op => op.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SQL-Server")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<MedicalContext>();
builder.Services.AddSingleton<IConverter, Converter>();
builder.Services.AddSingleton<IValidator, Validator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication? app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

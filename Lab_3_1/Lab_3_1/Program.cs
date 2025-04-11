using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Lab_3_1;

var builder = WebApplication.CreateBuilder(args);

SQLitePCL.Batteries.Init();

builder.Services.AddDbContext<ApplicationContext>(
    options =>
    {
        var config = builder.Configuration;
        options.UseSqlite(config.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    dbContext.Database.EnsureCreated();
    dbContext.SeedData();
}

app.Run();
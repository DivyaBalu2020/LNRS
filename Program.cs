using LNRS.Models;
using LNRS.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver.Core.Configuration;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<DatabaseSettings>(
        builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<IDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Debug()
         .ReadFrom.Configuration(builder.Configuration)
         .CreateLogger();

builder.Services.AddCors(o => o.AddDefaultPolicy(p => 
        p.WithOrigins("*").AllowAnyHeader()));

builder.Services.AddTransient<BlogService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

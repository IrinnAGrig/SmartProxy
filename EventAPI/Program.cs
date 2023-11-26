using Common.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using EventAPI.Repositories;
using EventAPI.Services;
using EventAPI.Settings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.Configure<SyncServiceSettings>(builder.Configuration.GetSection("SyncServiceSettings"));

builder.Services.AddSingleton<IMongoDbSettings>(provider => provider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
builder.Services.AddSingleton<ISyncServiceSettings>(provider => provider.GetRequiredService<IOptions<SyncServiceSettings>>().Value);

builder.Services.AddScoped<IMongoRepository<Event>, MongoRepository<Event>>();
builder.Services.AddScoped<ISyncService<Event>, SyncService<Event>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

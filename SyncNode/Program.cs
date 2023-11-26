using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SyncNode.Services;
using SyncNode.Settings;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.Configure<EventAPISettings>(builder.Configuration.GetSection("EventAPISettings"));

builder.Services.AddSingleton<IEventAPISettings>(provider => provider.GetRequiredService<IOptions<EventAPISettings>>().Value);


builder.Services.AddSingleton<SyncWorkJobService>();
builder.Services.AddHostedService(provider => provider.GetService<SyncWorkJobService>());

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();



app.UseAuthorization();

app.MapControllers();

app.Run();

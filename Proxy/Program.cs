using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Configuration
       .AddJsonFile("appsettings.json")
       .AddJsonFile("ocelot.json")
       .AddEnvironmentVariables();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                      builder => builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
});

builder.Services.AddOcelot();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();


app.UseOcelot().Wait();

app.Run();

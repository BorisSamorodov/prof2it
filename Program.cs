using BorisBot;
using BorisBot.Factories;
using BorisBot.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<BorisBotConfig>(builder.Configuration.GetSection("BorisBotConfig"));
builder.Services.AddTransient<IContextFactory, ContextFactory>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(e => e.MapControllers());

// migrations
var contextFactory =app.Services.GetRequiredService<IContextFactory>();
using var context = contextFactory.GetContext();
context.Database.Migrate();

app.Run();
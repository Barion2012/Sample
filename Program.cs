using Sample.Code;
using Sample.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options => { 
        options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    })
    .AddDbContext<PContext>(options => { 
        options.UseSqlServer(builder.Configuration.GetConnectionString("SampleDB"));
    }).AddScoped<IService, PService>();

var app = builder.Build();

app.MapApi();
app.UseStaticFiles();
app.MapFallbackToFile("/index.html");

app.Run();


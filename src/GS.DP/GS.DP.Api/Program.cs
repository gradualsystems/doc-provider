using GS.DP.Common;
using GS.DP.Facade;
using GS.DP.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions();

builder.Services.Configure<ProviderConfig>(builder.Configuration.GetSection("ProviderConfig"));

builder.Services.Configure<SimpleFileProviderConfig>(builder.Configuration.GetSection("SimpleFileProviderConfig"));

builder.Services.AddScoped<IDocProviderFacade, DocProviderFacade>();

builder.Services.AddScoped<IFileProvider, SimpleFileProvider>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .SetIsOriginAllowed((host) => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

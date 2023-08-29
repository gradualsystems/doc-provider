using GS.DP.Client;
using GS.DP.TestApi.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient("GS.DP.Client", c =>
{
    c.BaseAddress = new Uri("https://localhost:7120");
});

builder.Services.AddTransient<IFileProviderHttpClient, FileProviderHttpClient>();

builder.Services.AddTransient<ITestService, TestService>();

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

//app.MapGet("/", () => "Hello World!");

app.Run();

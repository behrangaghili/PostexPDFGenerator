using DinkToPdf;
using DinkToPdf.Contracts;
using MediatR;
using Microsoft.OpenApi.Models;
using Postex.receipt.Application.Configuration;
using Postex.receipt.Infrastrucre.Configuration;
using Postex.Receipt.Application;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "v1",
        Title = "receipt API"
    });
});



// Inject the API code here

//builder.Services.AddScoped<ServiceContainerResolver>();
//builder.Services.AddScoped<IServiceCollection, ServiceCollection>();
//builder.Services.AddScoped<IMediator, Mediator>();

//builder.Services.AddScoped<GenerateBarcodePdfResponse>();

// Add converter to DI
//builder.Services.AddSingleton(typeof(IConverter), new STASynchronizedConverter(new PdfTools()));
builder.Services.AddPersistance(builder.Configuration);
builder.Services.AddApplicationCore(builder.Configuration);

Log.Logger = new LoggerConfiguration()
   .WriteTo.File(
                "logs/logs-.log",
                rollingInterval: RollingInterval.Day,
                shared: true,
                flushToDiskInterval: TimeSpan.FromMinutes(15))
    .CreateBootstrapLogger();

builder.Host.UseSerilog();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/V1/swagger.json", "receipt.API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


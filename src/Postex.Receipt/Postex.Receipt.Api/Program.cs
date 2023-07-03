using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.OpenApi.Models;
using Postex.receipt.Application.Configuration;
using Postex.receipt.Infrastrucre.Configuration;
using Postex.Receipt.Application;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
// Add the registration for UnityContainerResolver
builder.Services.AddSingleton<UnityContainerResolver>();
// Add the registration for CreateReport
builder.Services.AddScoped<CreateReport>();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddSingleton<IConverter>(new SynchronizedConverter(new PdfTools()));




builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "v1",
        Title = "receipt API"
    });
});



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

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


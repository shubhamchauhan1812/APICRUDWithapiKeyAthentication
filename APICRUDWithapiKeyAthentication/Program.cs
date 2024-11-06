using APICRUDWithapiKeyAthentication.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;
using System.Globalization;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddScoped<ApiKeyService>(); // Register the ApiKeyService


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultStrings");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllers();

//Localization and Globalization service
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR")
    };

    options.DefaultRequestCulture = new RequestCulture("en-US");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog((hostingContext, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration);
});

// Add response compression services

builder.Services.AddResponseCompression(options =>

{

    options.EnableForHttps = true; // Enable response compression for HTTPS requests

    options.Providers.Add<BrotliCompressionProvider>();

    options.Providers.Add<GzipCompressionProvider>();

    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(

        new[] { "application/json" }); // Compress JSON responses

});



builder.Services.Configure<BrotliCompressionProviderOptions>(options =>

{

    options.Level = CompressionLevel.Fastest; // Set Brotli compression level

});



builder.Services.Configure<GzipCompressionProviderOptions>(options =>

{

    options.Level = CompressionLevel.Fastest; // Set Gzip compression level

});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        builder =>
        {
            builder.WithOrigins("https://localhost:44324", "https://localhost:7148", "http://localhost:46688")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Globalization and localization service
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>().Value;
app.UseRequestLocalization(localizationOptions);
// with a named pocili
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();
app.UseResponseCompression(); // Use response compression middleware

app.UseMiddleware<MiddlewareForApi>();

app.UseAuthorization();
app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.Run();

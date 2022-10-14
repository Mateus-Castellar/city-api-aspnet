using CityInfo.API;
using CityInfo.API.Data;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text;

//rollingInterval: RollingInterval.Day => gera um arquivo de log diariamente
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/cityInfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
builder.Host.UseSerilog();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Auth:Issuer"],
            ValidAudience = builder.Configuration["Auth:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                .GetBytes(builder.Configuration["Auth:Key"]))
        };
    });

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = false;
});

builder.Services.AddApiVersioning(setup =>
{
    setup.AssumeDefaultVersionWhenUnspecified = true;
    setup.DefaultApiVersion = new ApiVersion(1, 0);
    setup.ReportApiVersions = true;
});

builder.Services.AddDbContext<CityInfoContext>(options =>
    options.UseSqlite("Data Source=CityInfo.db"));

builder.Services.AddScoped<CitiesDataStore>();

builder.Services.AddTransient<ILocalMailService, LocalMailService>();
builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(setup =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setup.IncludeXmlComments(xmlCommentsFullPath);

    setup.AddSecurityDefinition("CityInfoApiBearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "input valid token to access endpoints API"
    });

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="CityInfoApiBearerAuth"
                }
            }, new List<string>()
        }
    });
});

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

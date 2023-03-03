using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using web_api_assignment.Models.Entities;
using web_api_assignment.Services.Franchises;
using web_api_assignment.Services.Characters;
using web_api_assignment.Services.Movies;

var builder = WebApplication.CreateBuilder(args);

//Configuring generated XML
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Muuuvie API",
        Description = "Simple API to manage movie characters in different movies and franchises",
        Contact = new OpenApiContact
        {
            Name = "Håvard Lund, Erik Skryseth",
            Url = new Uri("https://gitlab.com/NicholasLennox")
        },
        License = new OpenApiLicense
        {
            Name = "MIT 2023",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    options.IncludeXmlComments(xmlPath);
});

// Add services to the container.
builder.Services.AddDbContext<WebApiContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("MovieDB")
        )
    );


builder.Services.AddScoped<IFranchiseService, FranchiseServiceImpl>();
builder.Services.AddTransient<ICharacterService, CharacterServiceImpl>();
builder.Services.AddScoped<IMovieService, MovieServiceImpl>();

// Add automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Adding logger to the program cs file
builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

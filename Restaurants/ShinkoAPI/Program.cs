using System.Reflection;
using Microsoft.OpenApi.Models;
using ShinkoAPI.Business;
using ShinkoAPI.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("1.0", new OpenApiInfo
    {
        Version = "1.0",
        Title = "Restaurants API",
        Description = "An ASP.NET Core Web API for getting free reservations on some restaurants",
        Contact = new OpenApiContact
        {
            Name = "Thomas Blondel",
            Email = "thomas.foenix-blondel@epita.fr"
        }
    });
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Dependency injection
builder.Services.AddHttpClient();
builder.Services.AddTransient<IShinkoBusiness, ShinkoBusiness>();
builder.Services.AddTransient<IShinkoData, ShinkoData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/1.0/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
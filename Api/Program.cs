var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer(); // Necesario para generar documentación de endpoints
builder.Services.AddSwaggerGen(); // Agrega el generador de Swagger
builder.Services.AddControllers(); // Habilitar controladores

builder.Services.AddSwaggerGen(options => {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
                Title = "Mi API",
                Version = "v1",
                Description = "Descripción de mi API"
        });
});


var app = builder.Build();




app.UseSwagger(); // Habilita el middleware de Swagger
app.UseSwaggerUI(options =>
{
        // Configura los endpoints para Swagger
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        options.RoutePrefix = string.Empty; // Acceso en la raíz: http://localhost:5005/
});

app.UseHttpsRedirection();

app.Run();

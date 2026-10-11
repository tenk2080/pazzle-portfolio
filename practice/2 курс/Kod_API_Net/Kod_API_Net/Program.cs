using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using practice.Data;
using Swashbuckle.AspNetCore.SwaggerGen; 

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. Все регистрации сервисов — ДО builder.Build()
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); // Нужно для работы Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});

var app = builder.Build();

 


 
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

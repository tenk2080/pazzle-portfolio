using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;// Важно для OpenApiInfo

var builder = WebApplication.CreateBuilder(args);

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

// 2. Настройка пайплайна
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

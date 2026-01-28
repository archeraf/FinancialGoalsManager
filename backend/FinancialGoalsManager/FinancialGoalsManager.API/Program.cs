using FinancialGoalsManager.API.Middleware;
using FinancialGoalsManager.Infrastructure.Persistence.Context;
using FinancialGoalsManager.Infrastructure.Persistence.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
        opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<SqlServerContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    int retries = 5;
    while (retries > 0)
    {
        try
        {
            logger.LogInformation("Retrying to apply database migrations...");

            context.Database.Migrate();

            logger.LogInformation("Database ready!");
            break;
        }
        catch (Exception ex)
        {
            retries--;
            logger.LogWarning($"Error: {ex.Message}");

            if (retries == 0)
            {
                logger.LogCritical("After several retries, database wasn't created.");
                throw;
            }

            Thread.Sleep(5000);
        }
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Financial Goals Manager API");
        options.RoutePrefix = string.Empty;
    });
}

//app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();

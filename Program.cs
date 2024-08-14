using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Ioc.DependencyContainer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
string? envConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
if (!string.IsNullOrEmpty(envConnectionString))
{
    connectionString = envConnectionString;
}
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.RegisterServices();
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation(" migrating the database Started.");
        var context = services.GetRequiredService<AccountingDbContext>();
        context.Database.Migrate();

        var seedService = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
        logger.LogInformation(" migrating the database Done.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x.AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials()
                  .WithOrigins("http://localhost")
                  .SetIsOriginAllowed(origin => true));

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
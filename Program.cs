using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Validation.Account;
using EcoBar.Accounting.Core.Validation.AccountingFinancialYear;
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Repo.Classes;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
string envConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
if (!string.IsNullOrEmpty(envConnectionString))
{
    connectionString = envConnectionString;
}
builder.Services.AddDbContext<AccountingDbContext>(options =>
    options.UseSqlServer(connectionString));



#region Repository
builder.Services.AddTransient<IAccountingDocumentRepo, AccountingDocumentRepo>();
builder.Services.AddTransient<IAccountingFactorDetailRepo, AccountingFactorDetailRepo>();
builder.Services.AddTransient<IAccountingFactorSequenceCnfgRepo, AccountingFactorSequenceCnfgRepo>();
builder.Services.AddTransient<IAccountingFinancialYearRepo, AccountingFinancialYearRepo>();
builder.Services.AddTransient<IAccountingTransactionRepo, AccountingTransactionRepo>();
builder.Services.AddTransient<IAccountRepo, AccountRepo>();
builder.Services.AddTransient<ICompanyRepo, CompanyRepo>();
#endregion Repository

#region Services
builder.Services.AddTransient<IAccountantService, AccountantService>();
builder.Services.AddTransient<IFinancialYearService, FinancialYearService>();
#endregion

#region Validation
builder.Services.AddScoped<IValidator<BaseAccountDto>, AccountCreateValidation>();
builder.Services.AddScoped<IValidator<UpdateAccountDto>, AccountUpdateValidation>();
builder.Services.AddScoped<IValidator<BaseAccountIdDto>, AccountDeleteValidation>();
builder.Services.AddScoped<IValidator<BaseAccountIdDto>, AccountGetByIdValidation>();


builder.Services.AddScoped<IValidator<BaseFinancialYearIdDto>, FinancialYearGetByIdValidation>();
builder.Services.AddScoped<IValidator<CreateFinancialYearDto>, FinancialYearCreateValidation>();
builder.Services.AddScoped<IValidator<UpdateFinancialYearDto>, FinancialYearUpdateValidation>();
#endregion
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
        //await seedService.SeedAsync();
        logger.LogInformation(" migrating the database Done.");
    }
    catch (Exception ex)
    {
        // Handle exception

        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}




// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      .WithOrigins("http://localhost")
      .SetIsOriginAllowed(origin => true));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();


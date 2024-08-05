using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Validation.Account;
using EcoBar.Accounting.Core.Validation.AccountingFinancialYear;
using EcoBar.Accounting.Core.Validation.AccountTransaction;
using EcoBar.Accounting.Core.Validation.Company;
using EcoBar.Accounting.Core.Validation.Invoice;
using EcoBar.Accounting.Core.Validation.InvoiceItem;
using EcoBar.Accounting.Core.Validation.Item;
using EcoBar.Accounting.Core.Validation.Payment;
using EcoBar.Accounting.Core.Validation.Wallet;
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
builder.Services.AddTransient<IAccountingFinancialYearRepository, AccountingFinancialYearRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IAccountUserRepository, AccountUserRepository>();
builder.Services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
builder.Services.AddTransient<ICompanyRepository, CompanyRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<IInvoiceItemRepository, InvoiceItemRepository>();
builder.Services.AddTransient<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IWalletRepository, WalletRepository>();
#endregion Repository

#region Services
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IFinancialYearService, FinancialYearService>();
builder.Services.AddTransient<IAccountUserService, AccountUserService>();
builder.Services.AddTransient<IAccountTransactionService, AccountTransactionService>();
builder.Services.AddTransient<ICompanyService, CompanyService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IInvoiceItemService, InvoiceItemService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IWalletService, WalletService>();
#endregion

#region Validation

#region Account
builder.Services.AddScoped<IValidator<BaseAccountDto>, AccountCreateValidation>();
builder.Services.AddScoped<IValidator<UpdateAccountDto>, AccountUpdateValidation>();
builder.Services.AddScoped<IValidator<BaseAccountIdDto>, AccountDeleteValidation>();
builder.Services.AddScoped<IValidator<BaseAccountIdDto>, AccountGetByIdValidation>();
#endregion 

#region AccountTransaction
builder.Services.AddScoped<IValidator<AccountTransactionUserNameDto>, AccountTransactionUserNameValidation>();
builder.Services.AddScoped<IValidator<AccountTransactionNumberDto>, AccountTransactionNumberValidation>();
#endregion

#region FinancialYearId
builder.Services.AddScoped<IValidator<BaseFinancialYearIdDto>, FinancialYearGetByIdValidation>();
builder.Services.AddScoped<IValidator<CreateFinancialYearDto>, FinancialYearCreateValidation>();
builder.Services.AddScoped<IValidator<UpdateFinancialYearDto>, FinancialYearUpdateValidation>();
#endregion

#region Company
builder.Services.AddScoped<IValidator<CreateCompanyDto>, CreateCompanyValidation>();
#endregion

#region Item
builder.Services.AddScoped<IValidator<BaseItemDto>, CreateItemValidation>();
#endregion

#region InvoiceItem
builder.Services.AddScoped<IValidator<BaseInvoiceItemDto>, CreateInvoiceItemValidation>();
builder.Services.AddScoped<IValidator<UpdateInvoiceItemDto>, UpdateInvoiceItemValidation>();
builder.Services.AddScoped<IValidator<DeleteInvoiceItemDto>, DeleteInvoiceItemValidation>();
#endregion

#region Invoice
builder.Services.AddScoped<IValidator<CreateInvoiceDto>, CreateInvoiceValidation>();
builder.Services.AddScoped<IValidator<UpdateInvoiceDto>, UpdateInvoiceValidation>();
builder.Services.AddScoped<IValidator<DeleteInvoiceDto>, DeleteInvoiceValidation>();
builder.Services.AddScoped<IValidator<CloseInvoiceDto>, CloseInvoiceValidation>();
builder.Services.AddScoped<IValidator<PaymentInvoiceDto>, PaymentInvoiceValidation>();
#endregion

#region Payment
builder.Services.AddScoped<IValidator<CreatePaymentDto>, CreatePaymentValidation>();
#endregion

#region Wallet
builder.Services.AddScoped<IValidator<WalletGetByusernameDto>, WalletGetByusernameValidation>();
#endregion
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


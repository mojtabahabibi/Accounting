using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Validation.Account;
using EcoBar.Accounting.Core.Validation.AccountingFinancialYear;
using EcoBar.Accounting.Core.Validation.AccountTransaction;
using EcoBar.Accounting.Core.Validation.Company;
using EcoBar.Accounting.Core.Validation.Invoice;
using EcoBar.Accounting.Core.Validation.InvoiceItem;
using EcoBar.Accounting.Core.Validation.Payment;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Repo.Classes;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Ioc.DependencyContainer
{
    public static class DependencyContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            #region Repository
            services.AddTransient<IAccountingFinancialYearRepository, AccountingFinancialYearRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountUserRepository, AccountUserRepository>();
            services.AddTransient<IAccountTransactionRepository, AccountTransactionRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();
            #endregion Repository

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IFinancialYearService, FinancialYearService>();
            services.AddTransient<IAccountUserService, AccountUserService>();
            services.AddTransient<IAccountTransactionService, AccountTransactionService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IInvoiceItemService, InvoiceItemService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            #endregion

            #region Validation
            services.AddScoped<IValidator<BaseAccountDto>, AccountCreateValidation>();
            services.AddScoped<IValidator<UpdateAccountDto>, AccountUpdateValidation>();
            services.AddScoped<IValidator<BaseAccountIdDto>, AccountDeleteValidation>();
            services.AddScoped<IValidator<BaseAccountIdDto>, AccountGetByIdValidation>();

            services.AddScoped<IValidator<AccountTransactionUserNameDto>, AccountTransactionUserNameValidation>();
            services.AddScoped<IValidator<AccountTransactionNumberDto>, AccountTransactionNumberValidation>();

            services.AddScoped<IValidator<BaseFinancialYearIdDto>, FinancialYearGetByIdValidation>();
            services.AddScoped<IValidator<CreateFinancialYearDto>, FinancialYearCreateValidation>();
            services.AddScoped<IValidator<UpdateFinancialYearDto>, FinancialYearUpdateValidation>();

            services.AddScoped<IValidator<CreateCompanyDto>, CreateCompanyValidation>();

            services.AddScoped<IValidator<BaseInvoiceItemDto>, CreateInvoiceItemValidation>();
            services.AddScoped<IValidator<UpdateInvoiceItemDto>, UpdateInvoiceItemValidation>();
            services.AddScoped<IValidator<DeleteInvoiceItemDto>, DeleteInvoiceItemValidation>();

            services.AddScoped<IValidator<CreateInvoiceDto>, CreateInvoiceValidation>();
            services.AddScoped<IValidator<UpdateInvoiceDto>, UpdateInvoiceValidation>();
            services.AddScoped<IValidator<DeleteInvoiceDto>, DeleteInvoiceValidation>();
            services.AddScoped<IValidator<CloseInvoiceDto>, CloseInvoiceValidation>();
            services.AddScoped<IValidator<CancelInvoiceDto>, CancelInvoiceValidation>();
            services.AddScoped<IValidator<ReturnInvoiceDto>, ReturnInvoiceValidation>();
            services.AddScoped<IValidator<BuyChargeDto>, BuyChargeValidation>();
            services.AddScoped<IValidator<PaymentChargeDto>, PaymentChargeValidation>();

            services.AddScoped<IValidator<CreatePaymentDto>, CreatePaymentValidation>();
            services.AddScoped<IValidator<PaymentInvoiceDto>, PaymentInvoiceValidation>();
            #endregion

            services.AddAutoMapper(typeof(Program));
        }
    }
}
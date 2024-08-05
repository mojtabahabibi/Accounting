using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Payment
{
    public class PaymentInvoiceValidation : AbstractValidator<PaymentInvoiceDto>
    {
        public PaymentInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i=>i.InvoiceId).Must(id=>context.Invoices.Any(i=>i.Id.Equals(id)))
            .WithMessage("شماره فاکتور در سیستم وجود ندارد");
            RuleFor(i=>i.AccountId).Must(id=>context.Accounts.Any(i=>i.Id.Equals(id)))
            .WithMessage("شماره حساب در سیستم وجود ندارد");
        }
    }
}
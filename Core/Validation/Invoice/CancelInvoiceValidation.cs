using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class CancelInvoiceValidation : AbstractValidator<CancelInvoiceDto>
    {
        public CancelInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Id).Must(id => context.Invoices.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره فاکتور در سیستم وجود ندارد");
        }
    }
}

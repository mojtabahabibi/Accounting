using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class CloseInvoiceValidation : AbstractValidator<CloseInvoiceDto>
    {
        public CloseInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Id).Must(id => context.Invoices.Any(i => i.Id.Equals(id)))
               .WithMessage("آی دی فاکتور اشتباه است");
        }
    }
}

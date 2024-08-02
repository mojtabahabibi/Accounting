using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.InvoiceItem
{
    public class DeleteInvoiceItemValidation : AbstractValidator<DeleteInvoiceItemDto>
    {
        public DeleteInvoiceItemValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Id).Must(id => context.InvoiceItems.Any(i => i.Id.Equals(id))).WithMessage("آی دی اقلام فاکتور اشتباه است");
        }
    }
}
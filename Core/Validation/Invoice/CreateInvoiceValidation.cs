using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class CreateInvoiceValidation : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(id => context.AccountUsers.Any(i => i.Id.Equals(id))).WithMessage("آی دی خریدار اشتباه است");
            RuleFor(i => i.ComapnyId).Must(id => context.Companies.Any(i => i.Id.Equals(id))).WithMessage("آی دی شرکت اشتباه است");
        }
    }
}

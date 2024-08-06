using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class CreateInvoiceValidation : AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(id => context.AccountUsers.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره کاربر در سیستم وجود ندارد ");
            RuleFor(i => i.AccountUserId).Must(id => context.AccountUsers.Any(i => i.Id.Equals(id) && i.DeletedDate == null))
               .WithMessage("شماره کاربر در سیستم حذف شده است ");
        }
    }
}
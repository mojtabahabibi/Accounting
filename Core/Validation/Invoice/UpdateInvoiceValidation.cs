using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class UpdateInvoiceValidation : AbstractValidator<UpdateInvoiceDto>
    {
        public UpdateInvoiceValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Id).Must(id => context.Invoices.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره فاکتور اشتباه است");
            RuleFor(i => i.AccountUserId).Must(accountUserid => context.AccountUsers.Any(i => i.Id.Equals(accountUserid)))
                .WithMessage("شماره کاربر در سیستم وجود ندارد");
        }
    }
}
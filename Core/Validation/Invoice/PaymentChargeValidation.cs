using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class PaymentChargeValidation : AbstractValidator<PaymentChargeDto>
    {
        public PaymentChargeValidation(AccountingDbContext context)
        {
            RuleFor(i=>i.AccountUserId).Must(id=>context.AccountUsers.Any(i=>i.Id.Equals(id)))
                .WithMessage("شماره کاربری در سیستم وجود ندارد");   
        }
    }
}
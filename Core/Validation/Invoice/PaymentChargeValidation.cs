using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class PaymentChargeValidation : AbstractValidator<PaymentChargeDto>
    {
        public PaymentChargeValidation(AccountingDbContext context)
        {
            RuleFor(i=>i.UserId).Must(id=>context.Users.Any(i=>i.Id.Equals(id)))
                .WithMessage("شماره کاربری در سیستم وجود ندارد");   
        }
    }
}
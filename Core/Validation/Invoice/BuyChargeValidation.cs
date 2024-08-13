using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Invoice
{
    public class BuyChargeValidation : AbstractValidator<BuyChargeDto>
    {
        public BuyChargeValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(id => context.AccountUsers.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره کاربر در سیستم وجود ندارد");
            RuleFor(i => i.Price).GreaterThan(0).WithMessage("مقدار شارژ را بصورت صحیح وارد کنید");
        }
    }
}

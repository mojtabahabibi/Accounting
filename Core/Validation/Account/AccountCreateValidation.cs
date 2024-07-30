using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountCreateValidation : AbstractValidator<BaseAccountDto>
    {
        public AccountCreateValidation()
        {
            RuleFor(i => i.BaseUserId).NotNull().WithMessage("آی دی کاربر را وارد کنید");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب را وارد کنید");
            RuleFor(i => i.AccountNumber).NotNull().WithMessage("شماره حساب را وارد کنید");
        }
    }
}
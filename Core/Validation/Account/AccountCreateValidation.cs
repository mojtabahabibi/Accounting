using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountCreateValidation : AbstractValidator<BaseAccountDto>
    {
        public AccountCreateValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(AccountUserId => context.AccountUsers.Any(i => i.Id.Equals(AccountUserId)))
                .WithMessage("نام کاربری وارد شده اشتباه است");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب را وارد کنید");
            RuleFor(i => i.AccountNumber).NotNull().WithMessage("شماره حساب را وارد کنید");
        }
    }
}
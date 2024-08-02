using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountUpdateValidation : AbstractValidator<UpdateAccountDto>
    {
        public AccountUpdateValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(AccountUserId => context.AccountUsers.Any(i=>i.Id.Equals(AccountUserId)))
                .WithMessage("نام کاربری وارد شده اشتباه است");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب نمی تواند خالی باشد");
            RuleFor(i => i.AccountNumber).NotNull().WithMessage("شماره حساب نمی تواند خالی باشد");
        }
    }
}

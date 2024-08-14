using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountUpdateValidation : AbstractValidator<UpdateAccountDto>
    {
        public AccountUpdateValidation(AccountingDbContext context)
        {
            RuleFor(i => i.UserId).Must(UserId => context.Users.Any(i=>i.Id.Equals(UserId)))
                .WithMessage("نام کاربری وارد شده اشتباه است");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب نمی تواند خالی باشد");
            RuleFor(i => i.AccountNumber).NotNull().WithMessage("شماره حساب نمی تواند خالی باشد");
        }
    }
}

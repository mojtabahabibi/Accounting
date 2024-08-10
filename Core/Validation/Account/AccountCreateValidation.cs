using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountCreateValidation : AbstractValidator<BaseAccountDto>
    {
        private readonly IEnumerable<BaseAccountDto> _account;
        public AccountCreateValidation(IEnumerable<BaseAccountDto> account, AccountingDbContext context)
        {
            _account = account;

            RuleFor(i => i.AccountUserId).Must(AccountUserId => context.AccountUsers.Any(i => i.Id.Equals(AccountUserId)))
                .WithMessage("نام کاربری وارد شده اشتباه است");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب را وارد کنید");
            //RuleFor(i => i.AccountNumber).Must(IsAccountNumberUnique).WithMessage("شماره حساب نمی تواند تکراری باشد");
        }
        public bool IsAccountNumberUnique(BaseAccountDto editedPlayer, string newValue)
        {
            return _account.All(player => player.Equals(editedPlayer) || player.AccountNumber != newValue);
        }
    }
}
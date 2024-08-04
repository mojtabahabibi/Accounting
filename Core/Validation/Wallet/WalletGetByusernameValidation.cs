using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Wallet
{
    public class WalletGetByusernameValidation :AbstractValidator<WalletGetByusernameDto>
    {
        public WalletGetByusernameValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Username).Must(username => context.AccountUsers.Any(i => i.UserName.Equals(username))).
                WithMessage("نام کاربری مورد نظر در سیستم وجود ندارد");
        }
    }
}

using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.AccountTransaction
{
    public class AccountTransactionUserNameValidation : AbstractValidator<AccountTransactionUserNameDto>
    {
        public AccountTransactionUserNameValidation(AccountingDbContext context)
        {
            RuleFor(i=>i.AccountUserName).Must(username=>context.AccountUsers.Any(i=>i.UserName==username))
                .WithMessage("شماره کاربری مورد نظر هیچگونه تراکنشی ندارد");
        }
    }
}
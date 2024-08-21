using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Transactions
{
    public class TransactionsUserNameValidation : AbstractValidator<TransactionsUserNameDto>
    {
        public TransactionsUserNameValidation(AccountingDbContext context)
        {
            RuleFor(i=>i.UserName).Must(username=>context.Users.Any(i=>i.UserName==username))
                .WithMessage("شماره کاربری مورد نظر هیچگونه تراکنشی ندارد");
        }
    }
}
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.AccountTransaction
{
    public class AccountTransactionNumberValidation : AbstractValidator<AccountTransactionNumberDto>
    {
        public AccountTransactionNumberValidation(AccountingDbContext context)
        {
            RuleFor(i => i.TransactionNumber).Must(number => context.AccountTransactions.Any(i => i.TransactionNumber.Equals(number)))
                .WithMessage("شماره تراکنش مورد نظر یافت نشد");
        }
    }
}
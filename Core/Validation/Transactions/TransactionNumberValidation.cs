using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Transactions
{
    public class TransactionsNumberValidation : AbstractValidator<TransactionsNumberDto>
    {
        public TransactionsNumberValidation(AccountingDbContext context)
        {
            RuleFor(i => i.TransactionNumber).Must(number => context.Transactionss.Any(i => i.TransactionNumber.Equals(number)))
                .WithMessage("شماره تراکنش مورد نظر یافت نشد");
        }
    }
}
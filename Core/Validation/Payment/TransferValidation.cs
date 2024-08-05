using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Payment
{
    public class TransferValidation : AbstractValidator<TransferDto>
    {
        public TransferValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountCashId).Must(id => context.Accounts.Any(i => i.Id.Equals(id)))
            .WithMessage("شماره حساب نقدی در سیستم وجود ندارد");
            RuleFor(i => i.AccountWalletId).Must(id => context.Accounts.Any(i => i.Id.Equals(id)))
            .WithMessage("شماره حساب کیف پول در سیستم وجود ندارد");
        }
    }
}
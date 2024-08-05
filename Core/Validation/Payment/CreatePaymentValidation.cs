using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Payment
{
    public class CreatePaymentValidation : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountId).Must(id => context.Accounts.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره حساب در سیستم وجود ندارد");
        }
    }
}
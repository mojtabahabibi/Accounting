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
            RuleFor(i => i.AccountId).Must(id => context.Accounts.Any(i => i.Id.Equals(id) && i.AccountTypeId == 1))
                .WithMessage("شماره حساب وارد شده از نوع حساب نقدی نمی باشد");
            RuleFor(i => i.TransactionId).GreaterThan(0).WithMessage("مقدار وارد شده باید بیشتر از صفر باشد");
            RuleFor(i => i.Price).GreaterThan(0).WithMessage("مقدار وارد شده باید بیشتر از صفر باشد");
        }
    }
}
using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Payment
{
    public class CreatePaymentValidation : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentValidation(AccountingDbContext context)
        {
            RuleFor(i => i.AccountUserId).Must(id => context.AccountUsers.Any(i => i.Id.Equals(id)))
                .WithMessage("شماره کاربر اشتباه است");
        }
    }
}
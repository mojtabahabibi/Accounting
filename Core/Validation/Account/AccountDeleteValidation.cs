using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountDeleteValidation : AbstractValidator<BaseAccountIdDto>
    {
        public AccountDeleteValidation()
        {
            RuleFor(i => i.Id).NotNull().WithMessage("آی دی نمی تواند خالی باشد");
        }
    }
}
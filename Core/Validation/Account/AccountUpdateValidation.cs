using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class AccountUpdateValidation : AbstractValidator<UpdateAccountDto>
    {
        public AccountUpdateValidation()
        {
            RuleFor(i => i.BaseUserId).NotNull().WithMessage("آی دی نمی تواند خالی باشد");
            RuleFor(i => i.BaseUserId).NotNull().WithMessage("آی دی نمی تواند خالی باشد");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان حساب نمی تواند خالی باشد");
            RuleFor(i => i.AccountNumber).NotNull().WithMessage("شماره حساب نمی تواند خالی باشد");
        }
    }
}

using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.FinancialYear
{
    public class FinancialYearGetByIdValidation :AbstractValidator<BaseFinancialYearIdDto>
    {
        public FinancialYearGetByIdValidation()
        {
            RuleFor(i => i.Id).NotNull().WithMessage("آی دی نمی تواند خالی باشد");
        }
    }
}

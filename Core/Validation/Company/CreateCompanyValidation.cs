using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Company
{
    public class CreateCompanyValidation : AbstractValidator<CreateCompanyDto>
    {
        public CreateCompanyValidation()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("نام شرکت نمی تواند خالی باشد");
            RuleFor(i => i.Economicalnumber).NotEmpty().WithMessage("شماره اقتصادی نمی تواند خالی باشد");
        }
    }
}
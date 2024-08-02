using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.Item
{
    public class CreateItemValidation : AbstractValidator<BaseItemDto>
    {
        public CreateItemValidation()
        {
            RuleFor(i => i.Code).NotNull().WithMessage("کد محصول نمی تواند خالی باشد");
            RuleFor(i => i.Name).NotNull().WithMessage("نام محصول نمی تواند خالی باشد");
            RuleFor(i => i.Price).NotNull().WithMessage("قیمت محصول نمی تواند خالی باشد");
        }
    }
}
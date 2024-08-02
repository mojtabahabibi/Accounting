using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.InvoiceItem
{
    public class UpdateInvoiceItemValidation : AbstractValidator<UpdateInvoiceItemDto>
    {
        public UpdateInvoiceItemValidation(AccountingDbContext context)
        {
            RuleFor(i => i.Id).Must(Id => context.InvoiceItems.Any(i => i.Id.Equals(i.Id))).WithMessage("اقلام کالای انتخاب شده در سیستم وجود ندارد");
            RuleFor(i => i.ItemId).Must(itemId => context.Items.Any(i => i.Id.Equals(itemId))).WithMessage("کالای انتخاب شده در سیستم وجود ندارد");
            RuleFor(i => i.Count).NotNull().WithMessage("تعداد کالا را وارد کنید");
        }
    }
}

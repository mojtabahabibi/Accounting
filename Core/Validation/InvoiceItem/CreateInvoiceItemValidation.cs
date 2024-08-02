using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.InvoiceItem
{
    public class CreateInvoiceItemValidation :AbstractValidator<BaseInvoiceItemDto> 
    {
        public CreateInvoiceItemValidation(AccountingDbContext context)
        {
            RuleFor(i => i.ItemId).Must(itemId => context.Items.Any(i => i.Id.Equals(itemId))).WithMessage("کالای انتخاب شده در سیستم وجود ندارد");
            RuleFor(i => i.InvoiceId).Must(invoiceId => context.Invoices.Any(i => i.Id.Equals(invoiceId))).WithMessage("فاکتور انتخاب شده در سیستم وجود ندارد");
            RuleFor(i => i.Count).InclusiveBetween(0, 100000000).WithMessage("تعداد کالا را بصورت صحیح وارد کنید");
        }
    }
}
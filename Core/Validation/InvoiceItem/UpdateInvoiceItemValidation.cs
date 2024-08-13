using EcoBar.Accounting.Data.Configs;
using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.InvoiceItem
{
    public class UpdateInvoiceItemValidation : AbstractValidator<UpdateInvoiceItemDto>
    {
        public UpdateInvoiceItemValidation(AccountingDbContext context)
        {
            //RuleFor(i => i.ItemId).Must(itemId => context.Items.Any(i => i.Id.Equals(itemId)))
            //    .WithMessage("کالای انتخاب شده در سیستم وجود ندارد");
            //RuleFor(i => i.ItemId).Must(itemId => context.Items.Any(i => i.Id.Equals(itemId) && i.DeletedDate == null))
            //    .WithMessage("کالای انتخاب شده از سیستم حذف شده است");
            RuleFor(i => i.Id).Must(invoiceId => context.Invoices.Any(i => i.Id.Equals(invoiceId)))
                .WithMessage("فاکتور انتخاب شده در سیستم وجود ندارد");
            RuleFor(i => i.Id).Must(invoiceId => context.Invoices.Any(i => i.Id.Equals(invoiceId) && i.DeletedDate == null))
                .WithMessage("فاکتور انتخاب شده از سیستم حذف شده است");
        }
    }
}

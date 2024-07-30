﻿using EcoBar.Accounting.Data.Dto;
using FluentValidation;

namespace EcoBar.Accounting.Core.Validation.AccountingFinancialYear
{
    public class FinancialYearCreateValidation : AbstractValidator<CreateFinancialYearDto>
    {
        public FinancialYearCreateValidation()
        {
            RuleFor(i => i.StartAt).NotEmpty().WithMessage("تاریخ شروع سالی مالی نمی تواند خالی باشد")
                .LessThanOrEqualTo(i=>i.EndAt).WithMessage("تاریخ شروع سالی مالی نمی تواند بزرگتر از تاریخ پایان سالی مالی باشد");
            RuleFor(i => i.EndAt).NotEmpty().WithMessage("تاریخ پایان سالی مالی نمی تواند خالی باشد")
                .GreaterThanOrEqualTo(i => i.StartAt).WithMessage("تاریخ پایان سالی مالی باید بزرگتر از تاریخ شروع سالی مالی باشد");
            RuleFor(i => i.Title).NotNull().WithMessage("عنوان سالی مالی را وارد کنید");
        }
    }
}
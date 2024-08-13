using AutoMapper;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;

namespace EcoBar.Accounting.Data.Configs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Account
            CreateMap<Account, BaseAccountDto>().ReverseMap();
            CreateMap<Account, UpdateAccountDto>().ReverseMap();
            CreateMap<Account, BaseAccountIdDto>().ReverseMap();
            #endregion Account

            #region AccountUser
            CreateMap<AccountUser, CreateAccountUserDto>().ReverseMap();
            CreateMap<AccountUser, AccountUserListDto>().ReverseMap();
            #endregion AccountUser

            #region Company
            CreateMap<Company, CreateCompanyDto>().ReverseMap();
            #endregion

            #region FinancialYear
            CreateMap<AccountingFinancialYear, GetAllFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, GetByIdFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, CreateFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, UpdateFinancialYearDto>().ReverseMap();
            #endregion FinancialYear

            #region InvoiceItem
            CreateMap<InvoiceItem, BaseInvoiceItemDto>().ReverseMap();
            CreateMap<InvoiceItem, UpdateInvoiceItemDto>().ReverseMap();
            #endregion

            #region Invoice
            CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
            CreateMap<Invoice, UpdateInvoiceDto>().ReverseMap();
            CreateMap<Invoice, DeleteInvoiceDto>().ReverseMap();
            CreateMap<Invoice, InvoiceListDto>().ReverseMap();
            #endregion

            #region Payment
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            #endregion

            #region AccountTransaction
            CreateMap<AccountTransaction, AccountTransactionListDto>().ReverseMap();
            #endregion
        }

    }
}
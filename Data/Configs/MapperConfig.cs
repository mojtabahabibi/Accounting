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

            #region User
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserListDto>().ReverseMap();
            #endregion User

            #region Company
            CreateMap<Company, CreateCompanyDto>().ReverseMap();
            #endregion

            #region FinancialYear
            CreateMap<FinancialYear, GetAllFinancialYearDto>().ReverseMap();
            CreateMap<FinancialYear, GetByIdFinancialYearDto>().ReverseMap();
            CreateMap<FinancialYear, CreateFinancialYearDto>().ReverseMap();
            CreateMap<FinancialYear, UpdateFinancialYearDto>().ReverseMap();
            #endregion FinancialYear

            #region InvoiceItem
            CreateMap<InvoiceItem, BaseInvoiceItemDto>().ReverseMap();
            CreateMap<InvoiceItem, UpdateInvoiceItemDto>().ReverseMap();
            #endregion

            #region Invoice
            CreateMap<Invoice, CreateInvoiceDto>().ReverseMap();
            CreateMap<Invoice, UpdateInvoiceDto>().ReverseMap();
            CreateMap<Invoice, InvoiceListDto>().ReverseMap();
            #endregion

            #region Payment
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            #endregion

            #region Transactions
            CreateMap<Transactions, TransactionsListDto>().ReverseMap();
            #endregion
        }

    }
}
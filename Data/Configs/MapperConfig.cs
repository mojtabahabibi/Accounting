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

            #region FinancialYear
            CreateMap<AccountingFinancialYear, GetAllFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, GetByIdFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, CreateFinancialYearDto>().ReverseMap();
            CreateMap<AccountingFinancialYear, UpdateFinancialYearDto>().ReverseMap();
            #endregion FinancialYear
        }

    }
}
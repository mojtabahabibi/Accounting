using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IFinancialYearService
    {
        Task<GetAllFinancialYearResponseDto> GetAllFinancialYearAsync();
        Task<GetByIdFinancialYearResponseDto> GetByIdFinancialYearAsync(BaseFinancialYearIdDto dto);
        Task<BaseResponseDto<bool?>> CreateFinancialYearAsync(CreateFinancialYearDto dto);
        Task<BaseResponseDto<bool?>> UpdateFinancialYearAsync(UpdateFinancialYearDto dto);
        Task<BaseResponseDto<bool?>> SetActiveFinancialYearAsync(BaseFinancialYearIdDto dto);
        Task<BaseResponseDto<bool?>> SetCloseFinancialYearAsync(BaseFinancialYearIdDto dto);
        Task<BaseResponseDto<bool?>> DeleteFinancialYearAsync(BaseFinancialYearIdDto dto);
    }
}
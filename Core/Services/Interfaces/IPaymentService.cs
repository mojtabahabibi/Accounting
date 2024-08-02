using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<BaseResponseDto<bool?>> AddPaymentAsync(CreatePaymentDto dto);
    }
}

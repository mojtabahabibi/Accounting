using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class WalletService : IWalletService
    {
        private readonly ILogger<WalletService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<WalletGetByusernameDto> validator;
        private readonly IWalletRepository walletRepository;
        public WalletService(ILogger<WalletService> logger, IMapper mapper
            , IValidator<WalletGetByusernameDto> validator, IWalletRepository walletRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.validator = validator;
            this.walletRepository = walletRepository;
        }
        public async Task<WalletGetAllResponseDto> GetAllWallet()
        {
            logger.LogInformation("WalletService GetAllWallet Began");
            try
            {
                var result = await walletRepository.GetAllWalletAsync();
                var dto = mapper.Map<List<WalletListDto>>(result);
                logger.LogInformation("WalletService GetAllWallet Failed");
                return new WalletGetAllResponseDto()
                {
                    Data = dto,
                    DataCount = result.Count(),
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "WalletService GetAllWallet Failed");
                throw;
            }
        }
        public async Task<WalletGetByUsernameResponseDto> GetByUsername(WalletGetByusernameDto dto)
        {
            logger.LogInformation("WalletService GetByUsername Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new WalletGetByUsernameResponseDto();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await walletRepository.GetByUsernameAsync(dto.Username);
                    logger.LogInformation("WalletService GetByUsername Failed");

                    response.Data = result;
                    response.DataCount = 1;
                    response.ErrorCode = ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "دریافت شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "WalletService GetByUsername Failed");
                throw;
            }
        }
    }
}

using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Repo.Classes;
using EcoBar.Accounting.Data.Repo.Interfaces;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly ILogger<AccountTransactionService> logger;
        private readonly IMapper mapper;
        private readonly IAccountTransactionRepository accountTransactionRepository;
        public AccountTransactionService(ILogger<AccountTransactionService> logger , IMapper mapper, IAccountTransactionRepository accountTransactionRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.accountTransactionRepository = accountTransactionRepository;
        }
        public async Task<AccountTranasctionGetAllResponseDto> GetAllAccountTransactionAsync()
        {
            logger.LogInformation("AccountTransactionService GetAllInvioceItem Began");
            try
            {
                var result = await accountTransactionRepository.GetAllAccountTransactionAsync();
                logger.LogInformation("AccountTransactionService GetAllInvioceItem Failed");
                return new AccountTranasctionGetAllResponseDto()
                {
                    Data = result,
                    DataCount = result.Count(),
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionService GetAllInvioceItem Failed");
                throw;
            }
        }
    }
}

using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class AccountTransactionService : IAccountTransactionService
    {
        private readonly ILogger<AccountTransactionService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<AccountTransactionUserNameDto> usernameValidator;
        private readonly IValidator<AccountTransactionNumberDto> numberValidator;
        private readonly IAccountTransactionRepository accountTransactionRepository;
        public AccountTransactionService(ILogger<AccountTransactionService> logger, IMapper mapper
            , IValidator<AccountTransactionUserNameDto> usernameValidator, IValidator<AccountTransactionNumberDto> numberValidator,
            IAccountTransactionRepository accountTransactionRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.usernameValidator = usernameValidator;
            this.numberValidator = numberValidator;
            this.accountTransactionRepository = accountTransactionRepository;
        }
        public AccountTranasctionGetAllResponseDto GetAllAccountTransactionAsync()
        {
            logger.LogInformation("AccountTransactionService GetAllAccountTranasction Began");
            try
            {
                var result = accountTransactionRepository.GetAllAccountTransactionAsync();
                logger.LogInformation("AccountTransactionService GetAllAccountTranasction Failed");
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
                logger.LogError(ex, "AccountTransactionService GetAllAccountTranasction Failed");
                throw;
            }
        }
        public async Task<AccountTranasctionGetAllResponseDto> GetByUsernameAsync(string username)
        {
            logger.LogInformation("AccountTransactionService GetByUsername Began");
            try
            {
                var dto = new AccountTransactionUserNameDto() { AccountUserName = username };
                var validation = await usernameValidator.ValidateAsync(dto);
                var response = new AccountTranasctionGetAllResponseDto();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = accountTransactionRepository.GetByUserNameAsync(username);
                    logger.LogInformation("AccountTransactionService GetByUsername Failed");

                    response.Data = result;
                    response.DataCount = result.Count();
                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "دریافت شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionService GetByUsername Failed");
                throw;
            }
        }
        public async Task<AccountTranasctionGetByUsernameResponseDto> GetByTransactionNumberAsync(Guid number)
        {
            logger.LogInformation("AccountTransactionService GetByTransactionNumber Began");
            try
            {
                var dto = new AccountTransactionNumberDto() { TransactionNumber = number };
                var validation = await numberValidator.ValidateAsync(dto);
                var response = new AccountTranasctionGetByUsernameResponseDto();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await accountTransactionRepository.GetByTransactionNumberAsync(number);
                    logger.LogInformation("AccountTransactionService GetByTransactionNumber Failed");
                    response.Data = result;
                    response.DataCount = 1;
                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "دریافت شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTransactionService GetByTransactionNumber Failed");
                throw;
            }
        }
    }
}
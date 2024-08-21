using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ILogger<TransactionsService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<TransactionsUserNameDto> usernameValidator;
        private readonly IValidator<TransactionsNumberDto> numberValidator;
        private readonly ITransactionsRepository TransactionsRepository;
        public TransactionsService(ILogger<TransactionsService> logger, IMapper mapper
            , IValidator<TransactionsUserNameDto> usernameValidator, IValidator<TransactionsNumberDto> numberValidator,
            ITransactionsRepository TransactionsRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.usernameValidator = usernameValidator;
            this.numberValidator = numberValidator;
            this.TransactionsRepository = TransactionsRepository;
        }
        public async Task< AccountTranasctionGetAllResponseDto> GetAllTransactionsAsync()
        {
            logger.LogInformation("TransactionsService GetAllAccountTranasction Began");
            try
            {
                var result =await TransactionsRepository.GetAllTransactionsAsync();
                logger.LogInformation("TransactionsService GetAllAccountTranasction Failed");
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
                logger.LogError(ex, "TransactionsService GetAllAccountTranasction Failed");
                throw;
            }
        }
        public async Task<AccountTranasctionGetByAccountIdResponseDto> GetbyAccountIdTransactionAsync(long accountid)
        {
            logger.LogInformation("TransactionsService GetbyAccountIdTransaction Began");
            try
            {
                var result = await TransactionsRepository.GetByAccountIdTransactionAsync(accountid);
                logger.LogInformation("TransactionsService GetbyAccountIdTransaction Failed");
                return new AccountTranasctionGetByAccountIdResponseDto()
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
                logger.LogError(ex, "TransactionsService GetbyAccountIdTransaction Failed");
                throw;
            }
        }
        public async Task<AccountTranasctionGetAllResponseDto> GetByUsernameAsync(string username)
        {
            logger.LogInformation("TransactionsService GetByUsername Began");
            try
            {
                var dto = new TransactionsUserNameDto() { UserName = username };
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
                    var result =await TransactionsRepository.GetByUserNameAsync(username);
                    logger.LogInformation("TransactionsService GetByUsername Failed");

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
                logger.LogError(ex, "TransactionsService GetByUsername Failed");
                throw;
            }
        }
        public async Task<AccountTranasctionGetByUsernameResponseDto> GetByTransactionNumberAsync(string number)
        {
            logger.LogInformation("TransactionsService GetByTransactionNumber Began");
            try
            {
                var dto = new TransactionsNumberDto() { TransactionNumber = number };
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
                    var result = await TransactionsRepository.GetByTransactionNumberAsync(number);
                    logger.LogInformation("TransactionsService GetByTransactionNumber Failed");
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
                logger.LogError(ex, "TransactionsService GetByTransactionNumber Failed");
                throw;
            }
        }
    }
}
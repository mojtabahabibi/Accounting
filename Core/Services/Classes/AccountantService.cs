using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class AccountantService : IAccountantService
    {
        private readonly ILogger<AccountantService> logger;
        private readonly IValidator<BaseAccountDto> validator;
        private readonly IValidator<UpdateAccountDto> updateValidator;
        private readonly IValidator<BaseAccountIdDto> deleteValidator;
        private readonly IMapper mapper;
        private readonly IAccountRepo accountRepo;
        public AccountantService(ILogger<AccountantService> logger, IValidator<BaseAccountDto> validator, IValidator<UpdateAccountDto> updateValidator,
            IValidator<BaseAccountIdDto> deleteValidator, IMapper mapper, IAccountRepo accountRepo)
        {
            this.logger = logger;
            this.validator = validator;
            this.updateValidator = updateValidator;
            this.deleteValidator = deleteValidator;
            this.mapper = mapper;
            this.accountRepo = accountRepo;
        }
        public async Task<AccountCreateGetResponseDto> GetAllAccounts()
        {
            logger.LogInformation("AccountantService GetAllAccounts Began");
            try
            {
                var result = await accountRepo.GetAllAsync();
                List<BaseAccountDto> dto = mapper.Map<List<BaseAccountDto>>(result);
                logger.LogInformation("AccountantService GetAllAccounts Failed");
                return new AccountCreateGetResponseDto()
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
                logger.LogError(ex, "AccountantService GetAllAccounts Failed");
                throw;
            }
        }
        public async Task<AccountGetByIdResponseDto> GetByIdAccounts(BaseAccountIdDto dto)
        {
            logger.LogInformation("AccountantService GetAllAccounts Began");
            try
            {
                var result = await accountRepo.GetByIdAsync(dto.Id);
                BaseAccountDto Datadto = mapper.Map<BaseAccountDto>(result);
                logger.LogInformation("AccountantService GetAllAccounts Failed");
                return new AccountGetByIdResponseDto()
                {
                    Data = Datadto,
                    DataCount = 1,
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService GetAllAccounts Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateAccountAsync(BaseAccountDto dto)
        {
            logger.LogInformation("AccountantService GetAllAccounts Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await accountRepo.AddAsync(mapper.Map<Account>(dto));
                    logger.LogInformation("AccountantService GetAllAccounts Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService GetAllAccounts Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> UpdateAccountAsync(UpdateAccountDto dto)
        {
            logger.LogInformation("AccountantService UpdateAccounts Began");
            try
            {
                var validation = await updateValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await accountRepo.UpdateAsync(mapper.Map<Account>(dto));
                    logger.LogInformation("AccountantService UpdateAccounts Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ویرایش شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService UpdateAccounts Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> DeleteAccountAsync(BaseAccountIdDto dto)
        {
            logger.LogInformation("AccountantService DeleteAccounts Began");
            try
            {
                var validation = await deleteValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await accountRepo.DeleteAsync(dto.Id);
                    logger.LogInformation("AccountantService DeleteAccounts Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "حذف شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService DeleteAccounts Failed");
                throw;
            }
        }
    }
}
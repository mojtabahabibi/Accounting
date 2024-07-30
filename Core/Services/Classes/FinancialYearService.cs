using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class FinancialYearService : IFinancialYearService
    {
        private readonly ILogger<FinancialYearService> logger;
        private readonly IValidator<CreateFinancialYearDto> createValidator;
        private readonly IValidator<UpdateFinancialYearDto> updateValidator;
        private readonly IValidator<BaseFinancialYearIdDto> validator;
        private readonly IMapper mapper;
        private readonly IAccountingFinancialYearRepo accountingFinancialYearRepo;
        public FinancialYearService(ILogger<FinancialYearService> logger, IValidator<CreateFinancialYearDto> createValidator, IValidator<UpdateFinancialYearDto> updateValidator,
            IValidator<BaseFinancialYearIdDto> validator, IMapper mapper, IAccountingFinancialYearRepo accountingFinancialYearRepo)
        {
            this.logger = logger;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
            this.validator = validator;
            this.mapper = mapper;
            this.accountingFinancialYearRepo = accountingFinancialYearRepo;
        }
        public async Task<GetAllFinancialYearResponseDto> GetAllFinancialYearAsync()
        {
            logger.LogInformation("FinancialYearService GetAllFinancialYear Began");
            try
            {
                var acc = await accountingFinancialYearRepo.GetAllAsync();
                logger.LogInformation("FinancialYearService GetAllFinancialYear Done");
                List<GetAllFinancialYearDto> data = mapper.Map<List<GetAllFinancialYearDto>>(acc);
                return new GetAllFinancialYearResponseDto()
                {
                    Data = data,
                    DataCount = data.Count(),
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService GetAllFinancialYear Failed");
                throw;
            }
        }
        public async Task<GetByIdFinancialYearResponseDto> GetByIdFinancialYearAsync(BaseFinancialYearIdDto dto)
        {
            logger.LogInformation("FinancialYearService GetAllFinancialYear Began");
            try
            {
                var result = await accountingFinancialYearRepo.GetByIdAsync(dto.Id);
                logger.LogInformation("FinancialYearService GetAllFinancialYear Done");
                GetByIdFinancialYearDto data = mapper.Map<GetByIdFinancialYearDto>(result);
                return new GetByIdFinancialYearResponseDto()
                {
                    Data = data,
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "ایجاد شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService GetAllFinancialYear Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateFinancialYearAsync(CreateFinancialYearDto dto)
        {
            logger.LogInformation("FinancialYearService CreateFinancialYear Began");
            try
            {
                var validation = await createValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = "اعتبارسنجی معتبر نمی باشد";
                }
                else
                {
                    var acc = await accountingFinancialYearRepo.AddAsync(mapper.Map<AccountingFinancialYear>(dto));
                    logger.LogInformation("FinancialYearService CreateFinancialYear Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService CreateFinancialYear Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> UpdateFinancialYearAsync(UpdateFinancialYearDto dto)
        {
            logger.LogInformation("FinancialYearService UpdateFinancialYear Began");
            try
            {
                var validation = await updateValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = "اعتبارسنجی معتبر نمی باشد";
                }
                else
                {
                    var result = await accountingFinancialYearRepo.UpdateAsync(mapper.Map<AccountingFinancialYear>(dto));
                    logger.LogInformation("FinancialYearService UpdateFinancialYear Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.DataCount = 1;
                    response.Status = true;
                    response.Message = "ویرایش شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService UpdateFinancialYear Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> SetActiveFinancialYearAsync(BaseFinancialYearIdDto dto)
        {
            logger.LogInformation("FinancialYearService SetActiveFinancialYear Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = "اعتبارسنجی معتبر نمی باشد";
                }
                else
                {
                    var result = await accountingFinancialYearRepo.SetActive(dto);
                    if (result == FinancialYearActiveResult.Done)
                    {
                        logger.LogInformation("FinancialYearService SetActiveFinancialYear Done");

                        response.ErrorCode = Data.Enums.ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "سال مالی جاری بسته شد";
                    }
                    else if (result == FinancialYearActiveResult.NotFoundedFinancialYear)
                    {
                        logger.LogInformation("FinancialYearService SetActiveFinancialYear Failed");

                        response.ErrorCode = Data.Enums.ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "سال مالی جاری یافت نشد";
                    }
                    else if (result == FinancialYearActiveResult.IsCloseTrue)
                    {
                        logger.LogInformation("FinancialYearService SetActiveFinancialYear Failed");

                        response.ErrorCode = Data.Enums.ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "سال مالی جاری بسته شده است و نمی توان آن را فعال کرد";
                    }
                    else
                    {
                        logger.LogInformation("FinancialYearService SetActiveFinancialYear Failed");

                        response.ErrorCode = Data.Enums.ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "سال مالی جاری حذف شده است";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService SetActiveFinancialYear Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> SetCloseFinancialYearAsync(BaseFinancialYearIdDto dto)
        {
            logger.LogInformation("FinancialYearService SetCloseFinancialYear Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = "اعتبارسنجی معتبر نمی باشد";
                }
                else
                {
                    var result = await accountingFinancialYearRepo.SetClose(dto);
                    if (result)
                    {
                        logger.LogInformation("FinancialYearService SetCloseFinancialYear Done");

                        response.ErrorCode = Data.Enums.ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "سال مالی جاری فعال شد";
                    }
                    else
                    {
                        logger.LogInformation("FinancialYearService SetActiveFinancialYear Failed");

                        response.ErrorCode = Data.Enums.ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "سال مالی جاری یافت نشد";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantService SetActiveFinancialYear Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> DeleteFinancialYearAsync(BaseFinancialYearIdDto dto)
        {
            logger.LogInformation("FinancialYearService UpdateFinancialYear Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = "اعتبارسنجی معتبر نمی باشد";
                }
                else
                {
                    var result = await accountingFinancialYearRepo.DeleteAsync(dto.Id);
                    logger.LogInformation("FinancialYearService UpdateFinancialYear Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "حذف شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService UpdateFinancialYear Failed");
                throw;
            }
        }
    }
}

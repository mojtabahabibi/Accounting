using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class InvoiceItemService : IInvoiceItemService
    {
        private readonly ILogger<InvoiceItemService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<BaseInvoiceItemDto> createValidator;
        private readonly IValidator<UpdateInvoiceItemDto> updateValidator;
        private readonly IValidator<DeleteInvoiceItemDto> deleteValidator;
        private readonly IInvoiceItemRepository invoiceItemRepository;
        public InvoiceItemService(ILogger<InvoiceItemService> logger, IMapper mapper, IValidator<BaseInvoiceItemDto> createValidator, IValidator<UpdateInvoiceItemDto> updateValidator
            , IValidator<DeleteInvoiceItemDto> deleteValidator, IInvoiceItemRepository invoiceItemRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
            this.deleteValidator = deleteValidator;
            this.invoiceItemRepository = invoiceItemRepository;
        }
        public async Task<InvioceItemGetAllResponseDto> GetAllInvioceItemAsync()
        {
            logger.LogInformation("InvoiceItemService GetAllInvioceItem Began");
            try
            {
                var result = await invoiceItemRepository.GetAllAsync();
                var dto = mapper.Map<List<BaseInvoiceItemDto>>(result);
                logger.LogInformation("InvoiceItemService GetAllInvioceItem Failed");
                return new InvioceItemGetAllResponseDto()
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
                logger.LogError(ex, "InvoiceItemService GetAllInvioceItem Failed");
                throw;
            }
        }
        public async Task<InvioceItemGetByIdResponseDto> GetByIdInvioceItemAsync(BaseInvoiceItemIdDto dto)
        {
            logger.LogInformation("InvoiceItemService GetByIdInvioceItem Began");
            try
            {
                var result = await invoiceItemRepository.GetByIdAsync(dto.Id);
                var data = mapper.Map<BaseInvoiceItemDto>(result);
                logger.LogInformation("InvoiceItemService GetByIdInvioceItem Failed");
                return new InvioceItemGetByIdResponseDto()
                {
                    Data = data,
                    DataCount = 1,
                    ErrorCode = Data.Enums.ErrorCodes.OK,
                    Status = true,
                    Message = "دریافت شد"
                };
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemService GetByIdInvioceItem Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateInvoiceItemAsync(BaseInvoiceItemDto dto)
        {
            logger.LogInformation("InvoiceItemService CreateInvoiceItem Began");
            try
            {
                var validation = await createValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var invoiceStatus = await invoiceItemRepository.InvoiceStatus(dto.InvoiceId);
                    if (invoiceStatus == false)
                    {
                        var result = await invoiceItemRepository.CreateInvoiceItemAsync(mapper.Map<InvoiceItem>(dto));
                        logger.LogInformation("InvoiceItemService CreateInvoiceItem Done");
                        response.ErrorCode = Data.Enums.ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "ایجاد شد";
                    }
                    else
                    {
                        response.ErrorCode = Data.Enums.ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "فاکتور مورد نظر بسته شده است";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService UpdateInvoiceItem Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> UpdateInvoiceItemAsync(UpdateInvoiceItemDto dto)
        {
            logger.LogInformation("InvoiceItemService UpdateInvoiceItem Began");
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
                    await invoiceItemRepository.UpdateInvoiceItemAsync(mapper.Map<InvoiceItem>(dto));
                    logger.LogInformation("InvoiceItemService UpdateInvoiceItem Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ویرایش شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemService UpdateInvoiceItem Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> DeleteInvoiceItemAsync(DeleteInvoiceItemDto dto)
        {
            logger.LogInformation("InvoiceItemService DeleteInvoiceItem Began");
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
                    await invoiceItemRepository.DeleteAsync(dto.Id);
                    logger.LogInformation("InvoiceItemService DeleteInvoiceItem Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "حذف شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemService DeleteInvoiceItem Failed");
                throw;
            }
        }
    }
}
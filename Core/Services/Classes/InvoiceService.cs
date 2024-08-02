﻿using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<CreateInvoiceDto> createValidator;
        private readonly IValidator<UpdateInvoiceDto> updateValidator;
        private readonly IValidator<DeleteInvoiceDto> deleteValidator;
        private readonly IInvoiceRepository invoiceRepository;
        public InvoiceService(ILogger<InvoiceService> logger, IMapper mapper, IInvoiceRepository invoiceRepository, IValidator<CreateInvoiceDto> createValidator
            , IValidator<UpdateInvoiceDto> updateValidator, IValidator<DeleteInvoiceDto> deleteValidator)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.invoiceRepository = invoiceRepository;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
            this.deleteValidator = deleteValidator;
        }
        public async Task<GetAllInvoiceResponseDto> GetAllInvoiceAsync()
        {
            logger.LogInformation("InvoiceService GetAllInvoice Began");
            try
            {
                var result = await invoiceRepository.GetAllInvoiceAsync();
                var dto = mapper.Map<List<InvoiceListDto>>(result);
                logger.LogInformation("InvoiceService GetAllInvoice Failed");
                return new GetAllInvoiceResponseDto()
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
                logger.LogError(ex, "InvoiceService GetAllInvoice Failed");
                throw;
            }
        }
        public async Task<GetByIdInvoiceResponseDto> GetByIdInvoiceAsync(DeleteInvoiceDto dto)
        {
            logger.LogInformation("InvoiceService GetByIdInvoice Began");
            try
            {
                var result = await invoiceRepository.GetByIdInvoiceAsync(dto.Id);
                var response = new GetByIdInvoiceResponseDto();
                var data = mapper.Map<InvoiceListDto>(result);
                logger.LogInformation("InvoiceService GetByIdInvoice Failed");
                if (data == null)
                {
                    response.Data = null;
                    response.DataCount = 0;
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "آی دی فاکتور در سیستم وجود ندارد";
                }
                else
                {
                    response.Data = data;
                    response.DataCount = 1;
                    response.ErrorCode = ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "دریافت شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService GetByIdInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            logger.LogInformation("InvoiceService CreateInvoice Began");
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
                    var acc = await invoiceRepository.AddAsync(mapper.Map<Invoice>(dto));
                    logger.LogInformation("InvoiceService CreateInvoice Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService CreateInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> UpdateInvoiceAsync(UpdateInvoiceDto dto)
        {
            logger.LogInformation("InvoiceService UpdateInvoice Began");
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
                    var result = await invoiceRepository.UpdateAsync(mapper.Map<Invoice>(dto));
                    logger.LogInformation("InvoiceService UpdateInvoice Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ویرایش شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService UpdateInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> DeleteAccountAsync(DeleteInvoiceDto dto)
        {
            logger.LogInformation("InvoiceService DeleteInvoice Began");
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
                    var result = await invoiceRepository.DeleteAsync(dto.Id);
                    logger.LogInformation("InvoiceService DeleteInvoice Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "حذف شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService DeleteInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> PaymentAsync(long invoiceId)
        {
            logger.LogInformation("InvoiceService Payment Began");
            try
            {
                var response = new BaseResponseDto<bool?>();
                var result = await invoiceRepository.PaymentAsync(invoiceId);
                logger.LogInformation("InvoiceService Payment Done");
                if (result==PaymentResult.Done)
                {
                    response.ErrorCode = ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "پرداخت شد";
                }
                else if (result == PaymentResult.DontAccount)
                {
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "کاربر شماره حساب در سیستم ندارد";
                }
                else if (result == PaymentResult.DontMoney)
                {
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "کاربر پول کافی برای خرید ندارد";
                }
                else if (result == PaymentResult.DontInvoice)
                {
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "شماره فاکتور وجود ندارد";
                }
                else if (result == PaymentResult.DontWallet)
                {
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "کاربر کیف پول ندارد";
                }
                else if (result == PaymentResult.DontInvoiceItem)
                {
                    response.ErrorCode = ErrorCodes.NotFound;
                    response.Status = false;
                    response.Message = "فاکتور هیچگونه اقلام فاکتوری ندارد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService Payment Failed");
                throw;
            }
        }
    }
}

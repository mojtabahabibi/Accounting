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
    public class InvoiceService : IInvoiceService
    {
        private readonly ILogger<InvoiceService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<CreateInvoiceDto> createValidator;
        private readonly IValidator<UpdateInvoiceDto> updateValidator;
        private readonly IValidator<InvoiceIdDto> validator;
        private readonly IValidator<CreatePaymentDto> createPaymentValidator;
        private readonly IValidator<PaymentInvoiceDto> paymentValidator;
        private readonly IValidator<BuyChargeDto> chargeValidator;
        private readonly IValidator<PaymentChargeDto> paymentChargeValidator;
        private readonly IInvoiceRepository invoiceRepository;
        public InvoiceService(ILogger<InvoiceService> logger, IMapper mapper, IInvoiceRepository invoiceRepository, IValidator<CreateInvoiceDto> createValidator
            , IValidator<UpdateInvoiceDto> updateValidator, IValidator<InvoiceIdDto> validator, IValidator<CreatePaymentDto> createPaymentValidator, IValidator<PaymentInvoiceDto> paymentValidator
            , IValidator<BuyChargeDto> chargeValidator, IValidator<PaymentChargeDto> paymentChargeValidator)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.invoiceRepository = invoiceRepository;
            this.createValidator = createValidator;
            this.updateValidator = updateValidator;
            this.validator = validator;
            this.createPaymentValidator = createPaymentValidator;
            this.paymentValidator = paymentValidator;
            this.chargeValidator = chargeValidator;
            this.paymentChargeValidator = paymentChargeValidator;
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
        public async Task<GetByIdInvoiceResponseDto> GetByIdInvoiceAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService GetByIdInvoice Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new GetByIdInvoiceResponseDto();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.GetByIdInvoiceAsync(dto.Id);
                    var data = mapper.Map<InvoiceListDto>(result);
                    logger.LogInformation("InvoiceService GetByIdInvoice Failed");
                    if (data == null)
                    {
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "شماره فاکتور در سیستم وجود ندارد";
                    }
                    else
                    {
                        response.Data = data;
                        response.DataCount = 1;
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "دریافت شد";
                    }
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
        public async Task<BaseResponseDto<bool?>> DeleteAccountAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService DeleteInvoice Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.DeleteInvoiceAsync(dto.Id);
                    logger.LogInformation("InvoiceService DeleteInvoice Done");

                    response.ErrorCode = ErrorCodes.OK;
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
        public async Task<BaseResponseDto<bool?>> CloseInvoiceAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService UpdateInvoice Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.CloseInvoiceAsync(dto.Id);
                    if (result)
                    {
                        logger.LogInformation("InvoiceService UpdateInvoice Done");
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "فاکتور بسته شد";
                    }
                    else
                    {
                        logger.LogInformation("InvoiceService UpdateInvoice Failed");
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "فاکتور مورد نظر قبلا بسته شده است";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService UpdateInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> CancelAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService CancelInvoice Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.CancelInvoiceAsync(dto.Id);
                    switch (result)
                    {
                        case CancelInvoiceResult.CanceledBefor:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور از قبل لغو شده است";
                            return response;
                        case CancelInvoiceResult.CloseInvoice:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور بسته است";
                            return response;
                        case CancelInvoiceResult.PaymentBefor:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور قبلا پرداخت شده است";
                            return response;
                        case CancelInvoiceResult.Returnd:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "شماره فاکتور مرجوعی است";
                            return response;
                        case CancelInvoiceResult.UnFoundInvoice:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "شماره فاکتور در سیستم وجود ندارد";
                            return response;
                        case CancelInvoiceResult.Deleted:
                            logger.LogInformation("InvoiceService CancelInvoice Failed");
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور از سیستم حذف شده است";
                            return response;
                        case CancelInvoiceResult.Success:
                            logger.LogInformation("InvoiceService CancelInvoice Done");
                            response.ErrorCode = ErrorCodes.OK;
                            response.Status = true;
                            response.Message = "فاکتور کنسل شد";
                            return response;
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService CancelInvoice Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> ReturnAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService Deposit Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.ReturnedInvoiceAsync(dto.Id);
                    if (result)
                    {
                        logger.LogInformation("InvoiceService Deposit Done");
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "فاکتور مرجوع شد";
                    }
                    else
                    {
                        logger.LogInformation("InvoiceService Deposit Failed");
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "وضعیت فاکتور باید پرداخت شده باشد";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService Deposit Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> BuyChargeAsync(BuyChargeDto dto)
        {
            logger.LogInformation("InvoiceService BuyCharge Began");
            try
            {
                var validation = await chargeValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.BuyChargeAsync(dto);
                    logger.LogInformation("InvoiceService BuyCharge Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "فاکتور خرید شارژ ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService BuyCharge Failed");
                throw;
            }
        }
        public async Task<BaseResponseDto<bool?>> PaymentChargeAsync(PaymentChargeDto dto)
        {
            logger.LogInformation("InvoiceService Payment Began");
            try
            {
                var validation = await paymentChargeValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {

                    var result = await invoiceRepository.PaymentChargeAsync(dto);
                    logger.LogInformation("InvoiceService Payment Done");
                    if (result == PaymentResult.Done)
                    {
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "فاکتور پرداخت شد";
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
                        response.Message = "کاربر پول کافی برای خرید شارژ ندارد";
                    }
                    else if (result == PaymentResult.DontInvoice)
                    {
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "کاربر فاکتور خرید شارژی برای پرداخت ندارد";
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
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService Payment Failed");
                throw;
            }
        }
        public async Task<InvoiceStatusListResponseDto> InvoiceStatusListAsync(InvoiceIdDto dto)
        {
            logger.LogInformation("InvoiceService InvoiceStatusList Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new InvoiceStatusListResponseDto();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await invoiceRepository.InvoiceStatusListAsync(dto.Id);
                    logger.LogInformation("InvoiceService InvoiceStatusList Failed");
                    if (result.InvoiceStatusDtos == null)
                    {
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "شماره فاکتور هیچ سابقه ای نداشته است";
                    }
                    else
                    {
                        response.Data = result;
                        response.DataCount = 1;
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "اطلاعات دریافت شد";
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceService InvoiceStatusList Failed");
                throw;
            }
        }
    }
}
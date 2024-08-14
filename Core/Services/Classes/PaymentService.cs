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
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<CreatePaymentDto> createValidator;
        private readonly IValidator<PaymentInvoiceDto> paymentValidator;
        private readonly IPaymentRepository paymentRepository;
        public PaymentService(ILogger<PaymentService> logger, IMapper mapper, IValidator<CreatePaymentDto> createValidator
        ,  IValidator<PaymentInvoiceDto> paymentValidator,IPaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.createValidator = createValidator;
            this.paymentValidator = paymentValidator;
            this.paymentRepository = paymentRepository;
        }
        public async Task<BaseResponseDto<bool?>> DepositAsync(CreatePaymentDto dto)
        {
            logger.LogInformation("InvoiceService Deposit Began");
            try
            {
                var validation = await createValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await paymentRepository.DepositAsync(mapper.Map<Payment>(dto));
                    if (result)
                    {
                        logger.LogInformation("InvoiceService Deposit Done");
                        response.ErrorCode = ErrorCodes.OK;
                        response.Status = true;
                        response.Message = "واریز انجام شد";
                    }
                    else
                    {
                        logger.LogInformation("InvoiceService Deposit Failed");
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "شماره حساب مربوط به حساب کیف پول است";
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
        public async Task<BaseResponseDto<bool?>> PaymentAsync(PaymentInvoiceDto dto)
        {
            logger.LogInformation("PaymentService Payment Began");
            try
            {
                var validation = await paymentValidator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var result = await paymentRepository.PaymentAsync(dto);
                    logger.LogInformation("PaymentService Payment Done");
                    switch (result)
                    {
                        case PaymentResult.DontWallet:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "کاربر کیف پول ندارد";
                            break;
                        case PaymentResult.DontMoney:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "کاربر پول کافی برای خرید ندارد";
                            break;
                        case PaymentResult.DontAccount:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "کاربر شماره حساب در سیستم ندارد";
                            break;
                        case PaymentResult.DontInvoice:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "شماره فاکتور وجود ندارد";
                            break;
                        case PaymentResult.DontInvoiceItem:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور هیچگونه اقلام فاکتوری ندارد";
                            break;
                        case PaymentResult.InnerExeption:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "با خطای داخلی سیستم مواجه شده است";
                            break;
                        case PaymentResult.PayWithWallet:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "با حساب کیف پول خرید کنید";
                            break;
                        case PaymentResult.StatusInvoice:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "فاکتور انتخاب شده در وضعیت غیرقابل پرداخت است";
                            break;
                        case PaymentResult.MostPrice:
                            response.ErrorCode = ErrorCodes.NotFound;
                            response.Status = false;
                            response.Message = "قیمت وارد شده بیشتر از مقدار فاکتور است";
                            break;
                        case PaymentResult.Done:
                            response.ErrorCode = ErrorCodes.OK;
                            response.Status = true;
                            response.Message = "فاکتور پرداخت شد";
                            break;
                    }
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentService Payment Failed");
                throw;
            }
        }
    }
}
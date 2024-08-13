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
        public async Task<BaseResponseDto<bool?>> AddPaymentAsync(CreatePaymentDto dto)
        {
            logger.LogInformation("PaymentService AddPayment Began");
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
                    var acc = await paymentRepository.CreatePaymentAsync(mapper.Map<Payment>(dto));
                    logger.LogInformation("PaymentService AddPayment Done");

                    response.ErrorCode = ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentService CreateFinancialYear Failed");
                throw;
            }
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
                    if (result == PaymentResult.Done)
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
                    else if (result == PaymentResult.PayWithWallet)
                    {
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "با حساب کیف پول خرید کنید";
                    }
                    else if (result == PaymentResult.StatusInvoice)
                    {
                        response.ErrorCode = ErrorCodes.NotFound;
                        response.Status = false;
                        response.Message = "فاکتور انتخاب شده در وضعیت غیرقابل پرداخت است";
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
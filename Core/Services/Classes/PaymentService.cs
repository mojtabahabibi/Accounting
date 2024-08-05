using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Core.Validation.Payment;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Enums;
using EcoBar.Accounting.Data.Repo.Classes;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class PaymentService : IPaymentService
    {
        private readonly ILogger<PaymentService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<CreatePaymentDto> createValidator;
        private readonly IPaymentRepository paymentRepository;
        public PaymentService(ILogger<PaymentService> logger, IMapper mapper, IValidator<CreatePaymentDto> createValidator, IPaymentRepository paymentRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.createValidator = createValidator;
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
    }
}
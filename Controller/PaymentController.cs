using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> logger;
        private readonly IPaymentService paymentService;
        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            this.logger = logger;
            this.paymentService = paymentService;
        }
        [HttpPost("Deposit")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Deposit(CreatePaymentDto model)
        {
            logger.LogInformation("PaymentController Deposit Began");
            try
            {
                var result = await paymentService.DepositAsync(model);
                logger.LogInformation("PaymentController Deposit Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentController Deposit Began");
                if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
                return Ok(
                    new BaseResponseDto<bool>()
                    {
                        Status = false,
                        DataCount = 0,
                        ErrorCode = ex.errorCode,
                        Message = ex.Message
                    }
                );
            }
        }
        [HttpPost("Transfer")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Transfer(TransferDto model)
        {
            logger.LogInformation("PaymentController Transfer Began");
            try
            {
                var result = await paymentService.TransferAsync(model);
                logger.LogInformation("PaymentController Transfer Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentController Transfer Began");
                if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
                return Ok(
                    new BaseResponseDto<bool>()
                    {
                        Status = false,
                        DataCount = 0,
                        ErrorCode = ex.errorCode,
                        Message = ex.Message
                    }
                );
            }
        }
        [HttpPost("PaymentInvoice")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> PaymentInvoice(PaymentInvoiceDto model)
        {
            logger.LogInformation("PaymentController Transfer Began");
            try
            {
                var result = await paymentService.PaymentAsync(model);
                logger.LogInformation("PaymentController Transfer Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentController Transfer Began");
                if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
                return Ok(
                    new BaseResponseDto<bool>()
                    {
                        Status = false,
                        DataCount = 0,
                        ErrorCode = ex.errorCode,
                        Message = ex.Message
                    }
                );
            }
        }
    }
}

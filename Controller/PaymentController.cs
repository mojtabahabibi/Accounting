using EcoBar.Accounting.Core.Services.Classes;
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
        public PaymentController(ILogger<PaymentController> logger , IPaymentService paymentService)
        {
            this.logger = logger;
            this.paymentService = paymentService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreatePaymentDto model)
        {
            logger.LogInformation("PaymentController CreateNewPayment Began");
            try
            {
                var result = await paymentService.AddPaymentAsync(model);
                logger.LogInformation("PaymentController CreateNewPayment Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "PaymentController CreateNewPayment Began");
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

using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChargeController : ControllerBase
    {
        private readonly ILogger<ChargeController> logger;
        private readonly IInvoiceService invoiceService;
        public ChargeController(ILogger<ChargeController> logger, IInvoiceService invoiceService)
        {
            this.logger = logger;
            this.invoiceService = invoiceService;
        }
        [HttpPost("Buy")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> BuyCharge(BuyChargeDto dto)
        {
            logger.LogInformation("ChargeController BuyCharge Began");
            try
            {
                var result = await invoiceService.BuyChargeAsync(dto);
                logger.LogInformation("ChargeController BuyCharge Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "ChargeController BuyCharge Began");
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
        [HttpPost("PaymentCharge")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> PaymentCharge(PaymentChargeDto model)
        {
            logger.LogInformation("ChargeController PaymentCharge Began");
            try
            {
                var result = await invoiceService.PaymentChargeAsync(model);
                logger.LogInformation("ChargeController PaymentCharge Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "ChargeController PaymentCharge Began");
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
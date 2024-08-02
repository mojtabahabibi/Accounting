using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly ILogger<InvoiceController> logger;
        private readonly IInvoiceService invoiceService;
        public InvoiceController(ILogger<InvoiceController> logger, IInvoiceService invoiceService)
        {
            this.logger = logger;
            this.invoiceService = invoiceService;
        }
        [HttpGet("GetAll")]
        public  async Task<ActionResult<GetAllInvoiceResponseDto>> GetAll()
        {
            logger.LogInformation("InvoiceController GetAllInvoice Began");
            try
            {
                var result = await invoiceService.GetAllInvoiceAsync();
                logger.LogInformation("InvoiceController GetAllInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController GetAllInvoice Began");
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
        [HttpGet("GetById")]
        public async Task<ActionResult<GetByIdInvoiceResponseDto>> GetById([FromQuery]DeleteInvoiceDto dto)
        {
            logger.LogInformation("InvoiceController GetByIdInvoice Began");
            try
            {
                var result = await invoiceService.GetByIdInvoiceAsync(dto);
                logger.LogInformation("InvoiceController GetByIdInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController GetByIdInvoice Began");
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
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreateInvoiceDto model)
        {
            logger.LogInformation("InvoiceController CreateInvoice Began");
            try
            {
                var result = await invoiceService.CreateInvoiceAsync(model);
                logger.LogInformation("InvoiceController CreateInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController CreateInvoice Began");
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
        [HttpPut("Update")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Update(UpdateInvoiceDto model)
        {
            logger.LogInformation("InvoiceController UpdateInvoice Began");
            try
            {
                var result = await invoiceService.UpdateInvoiceAsync(model);
                logger.LogInformation("InvoiceController UpdateInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController UpdateInvoice Began");
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
        [HttpDelete("Delete")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Delete(DeleteInvoiceDto model)
        {
            logger.LogInformation("InvoiceController DeleteInvoice Began");
            try
            {
                var result = await invoiceService.DeleteAccountAsync(model);
                logger.LogInformation("InvoiceController DeleteInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController DeleteInvoice Began");
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
        [HttpPost("Payment")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Payment(long invoiceId)
        {
            logger.LogInformation("InvoiceController DeleteInvoice Began");
            try
            {
                var result = await invoiceService.PaymentAsync(invoiceId);
                logger.LogInformation("InvoiceController DeleteInvoice Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceController DeleteInvoice Began");
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

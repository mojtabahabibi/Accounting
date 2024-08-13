using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemController : ControllerBase
    {
        private readonly ILogger<InvoiceItemController> logger;
        private readonly IInvoiceItemService invoiceItemService;
        public InvoiceItemController(ILogger<InvoiceItemController> logger, IInvoiceItemService invoiceItemService)
        {
            this.logger = logger;
            this.invoiceItemService = invoiceItemService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<InvioceItemGetAllResponseDto>> GetAll()
        {
            logger.LogInformation("InvoiceItemController GetAllInvioceItem Began");
            try
            {
                var result = await invoiceItemService.GetAllInvioceItemAsync();
                logger.LogInformation("InvoiceItemController GetAllInvioceItem Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemController GetAllInvioceItem Began");
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
        public async Task<ActionResult<InvioceItemGetByIdResponseDto>> GetById([FromQuery] BaseInvoiceItemIdDto dto)
        {
            logger.LogInformation("InvoiceItemController GetByIdAccounts Began");
            try
            {
                var result = await invoiceItemService.GetByIdInvioceItemAsync(dto);
                logger.LogInformation("InvoiceItemController GetByIdAccounts Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemController GetByIdAccounts Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(BaseInvoiceItemDto model)
        {
            logger.LogInformation("InvoiceItemController CreateInvoiceItem Began");
            try
            {
                var result = await invoiceItemService.CreateInvoiceItemAsync(model);
                logger.LogInformation("InvoiceItemController CreateInvoiceItem Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemController CreateInvoiceItem Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Update(UpdateInvoiceItemDto model)
        {
            logger.LogInformation("InvoiceItemController UpdateInvoiceItem Began");
            try
            {
                var result = await invoiceItemService.UpdateInvoiceItemAsync(model);
                logger.LogInformation("InvoiceItemController UpdateInvoiceItem Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemController UpdateInvoiceItem Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Delete(DeleteInvoiceItemDto model)
        {
            logger.LogInformation("InvoiceItemController DeleteInvoiceItem Began");
            try
            {
                var result = await invoiceItemService.DeleteInvoiceItemAsync(model);
                logger.LogInformation("InvoiceItemController DeleteInvoiceItem Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "InvoiceItemController DeleteInvoiceItem Began");
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

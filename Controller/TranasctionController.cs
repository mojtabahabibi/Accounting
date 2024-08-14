using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranasctionController : ControllerBase
    {
        private readonly ILogger<TranasctionController> logger;
        private readonly ITransactionsService TransactionsService;
        public TranasctionController(ILogger<TranasctionController> logger, ITransactionsService TransactionsService)
        {
            this.logger = logger;
            this.TransactionsService = TransactionsService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<AccountTranasctionGetAllResponseDto>> GetAll()
        {
            logger.LogInformation("TranasctionController GetAllTransactions Began");
            try
            {
                var result =await  TransactionsService.GetAllTransactionsAsync();
                logger.LogInformation("TranasctionController GetAllTransactions Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TranasctionController GetAllTransactions Began");
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
        [HttpGet("GetByAccountId")]
        public async Task<ActionResult<AccountTranasctionGetByAccountIdResponseDto>> GetByAccountId([FromQuery] long accountid)
        {
            logger.LogInformation("TranasctionController GetByAccountId Began");
            try
            {
                var result = await TransactionsService.GetbyAccountIdTransactionAsync(accountid);
                logger.LogInformation("TranasctionController GetByAccountId Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TranasctionController GetByAccountId Began");
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
        [HttpGet("GetByUsername")]
        public async Task<ActionResult<AccountTranasctionGetAllResponseDto>> GetByUsername([FromQuery]TransactionsUserNameDto dto)
        {
            logger.LogInformation("TranasctionController GetByUsername Began");
            try
            {
                var result = await TransactionsService.GetByUsernameAsync(dto.UserName);
                logger.LogInformation("TranasctionController GetByUsername Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TranasctionController GetByUsername Began");
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
        [HttpGet("GetByTransactionNumber")]
        public async Task<ActionResult<AccountTranasctionGetByUsernameResponseDto>> GetByTransactionNumber([FromQuery]TransactionsNumberDto dto)
        {
            logger.LogInformation("TranasctionController GetByTransactionNumber Began");
            try
            {
                var result = await TransactionsService.GetByTransactionNumberAsync(dto.TransactionNumber);
                logger.LogInformation("TranasctionController GetByTransactionNumber Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "TranasctionController GetByTransactionNumber Began");
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
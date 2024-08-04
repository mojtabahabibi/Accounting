using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTranasctionController : ControllerBase
    {
        private readonly ILogger<AccountTranasctionController> logger;
        private readonly IAccountTransactionService accountTransactionService;
        public AccountTranasctionController(ILogger<AccountTranasctionController> logger, IAccountTransactionService accountTransactionService)
        {
            this.logger = logger;
            this.accountTransactionService = accountTransactionService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<AccountTranasctionGetAllResponseDto>> GetAll()
        {
            logger.LogInformation("AccountTranasctionController GetAllAccountTransaction Began");
            try
            {
                var result = await accountTransactionService.GetAllAccountTransactionAsync();
                logger.LogInformation("AccountTranasctionController GetAllAccountTransaction Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTranasctionController GetAllAccountTransaction Began");
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
        public async Task<ActionResult<AccountTranasctionGetAllResponseDto>> GetByUsername([FromQuery]AccountTransactionUserNameDto dto)
        {
            logger.LogInformation("AccountTranasctionController GetByUsername Began");
            try
            {
                var result = await accountTransactionService.GetByUsernameAsync(dto.AccountUserName);
                logger.LogInformation("AccountTranasctionController GetByUsername Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTranasctionController GetByUsername Began");
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
        public async Task<ActionResult<AccountTranasctionGetByUsernameResponseDto>> GetByTransactionNumber([FromQuery]AccountTransactionNumberDto dto)
        {
            logger.LogInformation("AccountTranasctionController GetByTransactionNumber Began");
            try
            {
                var result = await accountTransactionService.GetByTransactionNumberAsync(dto.TransactionNumber);
                logger.LogInformation("AccountTranasctionController GetByTransactionNumber Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountTranasctionController GetByTransactionNumber Began");
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

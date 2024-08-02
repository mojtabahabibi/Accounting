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
    }
}

using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountUserController : ControllerBase
    {
        private readonly ILogger<AccountUserController> logger;
        private readonly IAccountUserService accountUserService;
        public AccountUserController(ILogger<AccountUserController> logger, IAccountUserService accountUserService)
        {
            this.logger = logger;
            this.accountUserService = accountUserService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreateAccountUserDto model)
        {
            logger.LogInformation("AccountUserController CreateAccountUser Began");
            try
            {
                var result = await accountUserService.CreateAccountUserAsync(model);
                logger.LogInformation("AccountUserController CreateAccountUser Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountUserController CreateAccountUser Began");
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

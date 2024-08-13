using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService accountantService;
        public AccountController(ILogger<AccountController> logger, IAccountService accountantService)
        {
            this.logger = logger;
            this.accountantService = accountantService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<AccountCreateGetResponseDto>> GetAll()
        {
            logger.LogInformation("AccountController GetAllAccounts Began");
            try
            {
                var result = await accountantService.GetAllAccounts();
                logger.LogInformation("AccountController GetAllAccounts Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountController GetAllAccounts Began");
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
        public async Task<ActionResult<AccountGetByIdResponseDto>> GetById([FromQuery]BaseAccountIdDto dto)
        {
            logger.LogInformation("AccountController GetByIdAccounts Began");
            try
            {
                var result = await accountantService.GetByIdAccounts(dto);
                logger.LogInformation("AccountController GetByIdAccounts Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountController GetByIdAccounts Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(BaseAccountDto model)
        {
            logger.LogInformation("AccountController CreateNewAccount Began");
            try
            {
                var result = await accountantService.CreateAccountAsync(model);
                logger.LogInformation("AccountController CreateNewAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountController CreateNewAccount Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Update(UpdateAccountDto model)
        {
            logger.LogInformation("AccountController UpdateAccount Began");
            try
            {
                var result = await accountantService.UpdateAccountAsync(model);
                logger.LogInformation("AccountController UpdateAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountController UpdateAccount Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Delete(BaseAccountIdDto model)
        {
            logger.LogInformation("AccountController UpdateAccount Began");
            try
            {
                var result = await accountantService.DeleteAccountAsync(model);
                logger.LogInformation("AccountController UpdateAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountController UpdateAccount Began");
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
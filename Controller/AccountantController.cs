using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountantController : ControllerBase
    {
        private readonly ILogger<AccountantController> logger;
        private readonly IAccountantService accountantService;
        public AccountantController(ILogger<AccountantController> logger, IAccountantService accountantService)
        {
            this.logger = logger;
            this.accountantService = accountantService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<AccountCreateGetResponseDto>> GetAll()
        {
            logger.LogInformation("AccountantController GetAllAccounts Began");
            try
            {
                var result = await accountantService.GetAllAccounts();
                logger.LogInformation("AccountantController GetAllAccounts Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantController GetAllAccounts Began");
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
            logger.LogInformation("AccountantController GetByIdAccounts Began");
            try
            {
                var result = await accountantService.GetByIdAccounts(dto);
                logger.LogInformation("AccountantController GetByIdAccounts Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantController GetByIdAccounts Began");
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
            logger.LogInformation("AccountantController CreateNewAccount Began");
            try
            {
                var result = await accountantService.CreateAccountAsync(model);
                logger.LogInformation("AccountantController CreateNewAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantController CreateNewAccount Began");
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
            logger.LogInformation("AccountantController UpdateAccount Began");
            try
            {
                var result = await accountantService.UpdateAccountAsync(model);
                logger.LogInformation("AccountantController UpdateAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantController UpdateAccount Began");
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
            logger.LogInformation("AccountantController UpdateAccount Began");
            try
            {
                var result = await accountantService.DeleteAccountAsync(model);
                logger.LogInformation("AccountantController UpdateAccount Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "AccountantController UpdateAccount Began");
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
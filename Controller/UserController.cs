using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUserService UserService;
        public UserController(ILogger<UserController> logger, IUserService UserService)
        {
            this.logger = logger;
            this.UserService = UserService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<UserListResponseDto>> GetAll()
        {
            logger.LogInformation("UserController GetAllUser Began");
            try
            {
                var result = await UserService.GetAllUser();
                logger.LogInformation("UserController GetAllUser Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "UserController GetAllUser Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreateUserDto model)
        {
            logger.LogInformation("UserController CreateUser Began");
            try
            {
                var result = await UserService.CreateUserAsync(model);
                logger.LogInformation("UserController CreateUser Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "UserController CreateUser Began");
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

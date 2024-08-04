using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ILogger<WalletController> logger;
        private readonly IWalletService walletService;
        public WalletController(ILogger<WalletController> logger , IWalletService walletService)
        {
            this.logger = logger;
            this.walletService = walletService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<WalletGetAllResponseDto>> GetAll()
        {
            logger.LogInformation("WalletController GetAllWallets Began");
            try
            {
                var result = await walletService.GetAllWallet();
                logger.LogInformation("WalletController GetAllWallets Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "WalletController GetAllWallets Began");
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
        public async Task<ActionResult<WalletGetByUsernameResponseDto>> GetByUsername([FromQuery]WalletGetByusernameDto dto)
        {
            logger.LogInformation("WalletController GetByUsername Began");
            try
            {
                var result = await walletService.GetByUsername(dto);
                logger.LogInformation("WalletController GetByUsername Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "WalletController GetByUsername Began");
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
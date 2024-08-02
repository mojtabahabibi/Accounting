using EcoBar.Accounting.Core.Services.Classes;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<AccountingFinancialYearController> logger;
        private readonly IItemService itemService;
        public ItemController(ILogger<AccountingFinancialYearController> logger, IItemService itemService)
        {
            this.logger = logger;
            this.itemService = itemService;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<GetAllItemResponseDto>> GetAll()
        {
            logger.LogInformation("ItemController GetAllItem Began");
            try
            {
                var result = await itemService.GetAllItemAsync();
                logger.LogInformation("ItemController GetAllItem Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "ItemController GetAllItem Began");
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
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(BaseItemDto model)
        {
            logger.LogInformation("ItemController CreateNewAccountFinancialYear Began");
            try
            {
                var result = await itemService.CreateItemAsync(model);
                logger.LogInformation("ItemController CreateNewAccountFinancialYear Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "ItemController CreateNewAccountFinancialYear Began");
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

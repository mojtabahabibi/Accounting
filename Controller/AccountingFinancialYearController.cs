using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class AccountingFinancialYearController : ControllerBase
    //{
    //    private readonly ILogger<AccountingFinancialYearController> logger;
    //    private readonly IFinancialYearService financialYearService;
    //    public AccountingFinancialYearController(ILogger<AccountingFinancialYearController> logger, IFinancialYearService financialYearService)
    //    {
    //        this.logger = logger;
    //        this.financialYearService = financialYearService;
    //    }

    //    [HttpGet("GetAll")]
    //    public async Task<ActionResult<GetAllFinancialYearResponseDto>> GetAll()
    //    {
    //        logger.LogInformation("AccountingFinancialYearController GetAllAccountFinancialYear Began");
    //        try
    //        {
    //            var res = await financialYearService.GetAllFinancialYearAsync();
    //            logger.LogInformation("AccountingFinancialYearController GetAllAccountFinancialYear Done");
    //            return res;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController GetAllAccountFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //    [HttpGet("GetById")]
    //    public async Task<ActionResult<GetByIdFinancialYearResponseDto>> GetById([FromQuery]BaseFinancialYearIdDto dto)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController GetAllAccountFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.GetByIdFinancialYearAsync(dto);
    //            logger.LogInformation("AccountingFinancialYearController GetAllAccountFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController GetAllAccountFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }

    //    [HttpPost("Create")]
    //    public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreateFinancialYearDto model)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController CreateNewAccountFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.CreateFinancialYearAsync(model);
    //            logger.LogInformation("AccountingFinancialYearController CreateNewAccountFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController CreateNewAccountFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //    [HttpPut("Update")]
    //    public async Task<ActionResult<BaseResponseDto<bool?>>> Update(UpdateFinancialYearDto model)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController UpdateFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.UpdateFinancialYearAsync(model);
    //            logger.LogInformation("AccountingFinancialYearController UpdateFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController UpdateFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //    [HttpPut("SetActive")]
    //    public async Task<ActionResult<BaseResponseDto<bool?>>> SetActive(BaseFinancialYearIdDto model)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController SetActiveFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.SetActiveFinancialYearAsync(model);
    //            logger.LogInformation("AccountingFinancialYearController SetActiveFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController SetActiveFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //    [HttpPut("SetClose")]
    //    public async Task<ActionResult<BaseResponseDto<bool?>>> SetClose(BaseFinancialYearIdDto model)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController SetActiveFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.SetCloseFinancialYearAsync(model);
    //            logger.LogInformation("AccountingFinancialYearController SetActiveFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController SetActiveFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //    [HttpDelete("Delete")]
    //    public async Task<ActionResult<BaseResponseDto<bool?>>> Delete(BaseFinancialYearIdDto model)
    //    {
    //        logger.LogInformation("AccountingFinancialYearController DeleteFinancialYear Began");
    //        try
    //        {
    //            var result = await financialYearService.DeleteFinancialYearAsync(model);
    //            logger.LogInformation("AccountingFinancialYearController DeleteFinancialYear Done");
    //            return result;
    //        }
    //        catch (AccountingException ex)
    //        {
    //            logger.LogError(ex, "AccountingFinancialYearController DeleteFinancialYear Began");
    //            if (ex.IsSystemError) return StatusCode((int)ex.errorCode, ex.Message);
    //            return Ok(
    //                new BaseResponseDto<bool>()
    //                {
    //                    Status = false,
    //                    DataCount = 0,
    //                    ErrorCode = ex.errorCode,
    //                    Message = ex.Message
    //                }
    //            );
    //        }
    //    }
    //}
}

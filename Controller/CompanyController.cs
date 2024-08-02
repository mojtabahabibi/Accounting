using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EcoBar.Accounting.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> logger;
        private readonly ICompanyService companyService;
        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService)
        {
            this.logger = logger;
            this.companyService = companyService;
        }
        [HttpPost("Create")]
        public async Task<ActionResult<BaseResponseDto<bool?>>> Create(CreateCompanyDto model)
        {
            logger.LogInformation("CompanyController CreateCompany Began");
            try
            {
                var result = await companyService.CreateCompanyAsync(model);
                logger.LogInformation("CompanyController CreateCompany Done");
                return result;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "CompanyController CreateCompany Began");
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

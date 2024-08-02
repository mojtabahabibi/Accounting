using AutoMapper;
using EcoBar.Accounting.Core.Services.Interfaces;
using EcoBar.Accounting.Core.Tools;
using EcoBar.Accounting.Data.Dto;
using EcoBar.Accounting.Data.Entities;
using EcoBar.Accounting.Data.Repo.Interfaces;
using FluentValidation;

namespace EcoBar.Accounting.Core.Services.Classes
{
    public class CompanyService : ICompanyService
    {
        private readonly ILogger<CompanyService> logger;
        private readonly IMapper mapper;
        private readonly IValidator<CreateCompanyDto> validator;
        private readonly ICompanyRepository companyRepository;
        public CompanyService(ILogger<CompanyService> logger, IMapper mapper, IValidator<CreateCompanyDto> validator,ICompanyRepository companyRepository)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.validator = validator;
            this.companyRepository = companyRepository;
        }
        public async Task<BaseResponseDto<bool?>> CreateCompanyAsync(CreateCompanyDto dto)
        {
            logger.LogInformation("ItemService CreateFinancialYear Began");
            try
            {
                var validation = await validator.ValidateAsync(dto);
                var response = new BaseResponseDto<bool?>();
                if (!validation.IsValid)
                {
                    response.ErrorCode = Data.Enums.ErrorCodes.BadRequest;
                    response.Status = false;
                    response.Message = validation.Errors.Select(i => i.ErrorMessage).First();
                }
                else
                {
                    var acc = await companyRepository.AddComapnyAsync(mapper.Map<Company>(dto));
                    logger.LogInformation("ItemService CreateFinancialYear Done");

                    response.ErrorCode = Data.Enums.ErrorCodes.OK;
                    response.Status = true;
                    response.Message = "ایجاد شد";
                }
                return response;
            }
            catch (AccountingException ex)
            {
                logger.LogError(ex, "FinancialYearService CreateFinancialYear Failed");
                throw;
            }
        }
    }
}

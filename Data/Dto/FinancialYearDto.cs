namespace EcoBar.Accounting.Data.Dto
{
    public class BaseFinancialYearDto
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Title { get; set; }
    }
    public class BaseFinancialYearIdDto
    {
        public long Id { get; set; }
    }
    public class CreateFinancialYearDto : BaseFinancialYearDto
    {

    }
    public class UpdateFinancialYearDto : BaseFinancialYearDto
    {
        public long Id { get; set; }
    }
    public class GetAllFinancialYearDto : BaseFinancialYearDto
    {
        public bool IsActive { get; set; }
    }
    public class GetByIdFinancialYearDto : GetAllFinancialYearDto
    {

    }
    public class GetAllFinancialYearResponseDto : BaseResponseDto<List<GetAllFinancialYearDto>>
    {

    }
    public class GetByIdFinancialYearResponseDto : BaseResponseDto<GetByIdFinancialYearDto>
    {

    }
}
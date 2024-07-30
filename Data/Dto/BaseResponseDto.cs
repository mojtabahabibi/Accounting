using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Data.Dto
{
    public class BaseResponseDto<T>
    {
        public T? Data { get; set; } 
        public bool Status { get; set; }
        public string Message { get; set; } = "";
        public ErrorCodes ErrorCode { get; set; }
        public int? DataCount { get; set; }
    }
}
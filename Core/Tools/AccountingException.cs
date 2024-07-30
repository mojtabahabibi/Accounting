using EcoBar.Accounting.Data.Enums;

namespace EcoBar.Accounting.Core.Tools
{
    public class AccountingException : Exception
    {
        public bool IsSystemError = true;
        public ErrorCodes errorCode = ErrorCodes.BadRequest;
        public AccountingException(string? message, bool SystemError, ErrorCodes? errorCode) : base(message)
        {
            IsSystemError = SystemError;
            if (errorCode != null) this.errorCode = errorCode.Value;
        }
    }
}
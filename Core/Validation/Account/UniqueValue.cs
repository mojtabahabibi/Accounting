using EcoBar.Accounting.Data.Dto;

namespace EcoBar.Accounting.Core.Validation.Account
{
    public class UniqueValue
    {
        public bool IsAccountNumberUnique(IEnumerable<BaseAccountDto> accounts, BaseAccountDto editedAccount, string newValue)
        {
            return accounts.All(i => i.Equals(editedAccount) || i.AccountNumber != newValue);
        }
    }
}
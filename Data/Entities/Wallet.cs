﻿namespace EcoBar.Accounting.Data.Entities
{
    public class Wallet : BaseEntity
    {
        public long AccountId { get; set; }
        public Account Account { get; set; }
        public Guid WalletNumber { get; set; }
        public long Amount { get; set; }
    }
}
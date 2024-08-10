﻿namespace EcoBar.Accounting.Data.Entities
{
    public class AccountBook : BaseEntity
    {
        public long AccountTransactionId { get; set; }
        public required AccountTransaction AccountTransaction { get; set; }

        public long AccountId { get; set; }
        public required Account Account { get; set; }

        public long Amount { get; set; }
    }
}
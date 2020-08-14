using System;

namespace MS.Modular.AccountManagement.Domain.Accounts
{
    public class Account
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int PurchasedApplications { get; set; }
        public Account()
        {
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
        }
    }
}
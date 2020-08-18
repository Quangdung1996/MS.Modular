using System;

namespace MS.Modular.AccountManagement.Domain.Accounts
{
    public class Account
    {
        public int AccountId { get; private set; }

        public string Name { get; private set; }

        public DateTime DateCreated { get; private set; }

        public DateTime DateUpdated { get; private set; }

        public int PurchasedApplications { get; private set; }

        public Account()
        {
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
        }

        public Account(string name, int purchasedApplications)
        {
            this.Name = name;
            this.PurchasedApplications = purchasedApplications;
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
        }
    }
}
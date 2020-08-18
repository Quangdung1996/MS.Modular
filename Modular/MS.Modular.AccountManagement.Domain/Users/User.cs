﻿using MS.Modular.AccountManagement.Domain.Accounts;
using System;

namespace MS.Modular.AccountManagement.Domain.Users
{
    public class User
    {
        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public string PasswordSalt { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AccountId { get; set; }

        public int UserTypeId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public DateTime DateLastLogin { get; set; }

        public Account Account { get; set; }

        public UserType UserType { get; set; }

        public User()
        {
            this.DateCreated = DateTime.Now;
            this.DateUpdated = DateTime.Now;
            this.DateLastLogin = DateTime.Now;
        }
    }
}
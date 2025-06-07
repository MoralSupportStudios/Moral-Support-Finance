using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoralSupport.Finance.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int OrganizatoinId { get; set; }
        public Organization? Organization { get; set; } = null;
        public int UserId { get;  set; }
        public User? User { get; set; } = null;
        public int AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; } = null;
        public string AccountName { get; set; } = string.Empty;
        public bool IsBusiness { get; set; }
        public string? Institution { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; } = 0m;

    }
}

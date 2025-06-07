using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoralSupport.Finance.Domain.Entities
{
    public class AccountType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

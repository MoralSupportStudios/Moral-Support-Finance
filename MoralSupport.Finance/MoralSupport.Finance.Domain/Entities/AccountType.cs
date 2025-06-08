namespace MoralSupport.Finance.Domain.Entities
{
    public class AccountType
    {
        public int Id { get; set; }

        public string TypeName { get; set; }

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}

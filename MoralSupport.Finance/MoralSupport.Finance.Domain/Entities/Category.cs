namespace MoralSupport.Finance.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}

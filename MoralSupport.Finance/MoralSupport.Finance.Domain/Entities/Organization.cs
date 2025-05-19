namespace MoralSupport.Finance.Domain.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserOrganization> UserOrganizations { get; set; } = new List<UserOrganization>();

    }
}

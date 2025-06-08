namespace MoralSupport.Finance.Domain.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
        public string PropertyName { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public DateTime LastUsed { get; set; } = DateTime.UtcNow;
    }
}

namespace MoralSupport.Finance.Domain.Entities
{
    public class UserOrganization
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }

        public string Role { get; set; } = "Admin"; //TODO Talk to Nick about this
    }
}

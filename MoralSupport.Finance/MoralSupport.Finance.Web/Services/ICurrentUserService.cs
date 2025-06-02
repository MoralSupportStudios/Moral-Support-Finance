namespace MoralSupport.Finance.Web.Services
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Email { get; }
        string Name { get; }

    }
}

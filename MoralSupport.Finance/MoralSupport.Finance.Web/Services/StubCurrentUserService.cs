namespace MoralSupport.Finance.Web.Services
{
    public class StubCurrentUserService : ICurrentUserService
    {
        public int UserId => 1;
        public string Email => "testuser@example.com";
    }
}

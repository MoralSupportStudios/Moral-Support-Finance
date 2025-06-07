using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;

namespace MoralSupport.Finance.Web.Pages.Organizations.Users
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public IList<UserOrganization> UserOrganization { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out var parsedUserId))
            {
                UserOrganization = await _context.UserOrganizations
                    .Where(uo => uo.UserId == parsedUserId)
                    .Include(uo => uo.Organization)
                    .Include(uo => uo.User)
                    .ToListAsync();
            }
            else
            {
                UserOrganization = new List<UserOrganization>();
            }

        }
    }
}

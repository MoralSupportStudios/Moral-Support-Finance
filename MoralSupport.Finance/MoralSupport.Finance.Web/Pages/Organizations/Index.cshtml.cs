using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;

namespace MoralSupport.Finance.Web.Pages.Organizations
{
    public class IndexModel : PageModel
    {
        private readonly Infrastructure.Persistence.AppDbContext _context;

        public IndexModel(Infrastructure.Persistence.AppDbContext context)
        {
            _context = context;
        }

        public IList<Organization> Organizations { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (int.TryParse(userId, out var parseUserId))
            {
                Organizations = await _context.UserOrganizations
                                   .Where(uo => uo.UserId == parseUserId)
                                   .Include(uo => uo.Organization)
                                   .Select(uo => uo.Organization!)
                                   .ToListAsync();
            }
            else
            {
                Organizations = new List<Organization>();
            }
        }
    }
}

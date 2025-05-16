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

        public IList<Organization> Organizations { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Organizations = await _context.Organizations.ToListAsync();
        }
    }
}

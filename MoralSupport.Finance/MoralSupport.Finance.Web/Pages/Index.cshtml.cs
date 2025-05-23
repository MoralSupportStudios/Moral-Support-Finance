using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;
using MoralSupport.Finance.Web.Services;

namespace MoralSupport.Finance.Web.Pages
{
    public class IndexModel : PageModel
    {
        

        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
            
        }
        

        public IList<Organization> Organizations { get; set; } = default!;

        [TempData]
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {
            // Load organizations from the database
            Organizations = await _context.Organizations.ToListAsync();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Message = $"Welcome, {User.Identity.Name}!";
            }
                }

        public async Task<IActionResult> OnPostAsync(string organizationName)
        {
            if (string.IsNullOrEmpty(organizationName))
            {
                return Page();
            }

            // Add a new organization to the database
            var org = new Organization { Name = organizationName };
            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

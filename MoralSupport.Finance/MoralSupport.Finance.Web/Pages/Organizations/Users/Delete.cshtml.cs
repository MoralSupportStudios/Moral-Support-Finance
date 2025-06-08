using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;

namespace MoralSupport.Finance.Web.Pages.Organizations.Users
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserOrganization UserOrganization { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userorganization = await _context.UserOrganizations.FirstOrDefaultAsync(m => m.Id == id);

            if (userorganization == null)
            {
                return NotFound();
            }
            else
            {
                UserOrganization = userorganization;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userorganization = await _context.UserOrganizations.FindAsync(id);
            if (userorganization != null)
            {
                UserOrganization = userorganization;
                _context.UserOrganizations.Remove(UserOrganization);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

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
        private readonly ICurrentUserService _currentUser;

        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        public IList<Organization> Organizations { get; set; } = default!;

        [TempData]
        public string? Message { get; set; }

        public async Task OnGetAsync()
        {

            var userId = _currentUser.UserId;

            if (userId == 0)
            {
                Message = "You are not logged in.";
                return;
            }


            // Load organizations from the database
            Organizations = await _context.UserOrganizations
               .Where(uo => uo.UserId == userId)
               .Include(uo => uo.Organization)
               .Select(uo => uo.Organization!)
               .ToListAsync();

            Message = $"Welcome, {_currentUser.Name}!";


            
            
                }

        public async Task<IActionResult> OnPostAsync(string organizationName)
        {
            if (string.IsNullOrEmpty(organizationName))
            {
                return Page();
            }

              var userId = _currentUser.UserId;

            if (userId == 0)
            {
                Message = "You must be signed in to create an organization.";
                return RedirectToPage();
            }

            // Add a new organization to the database
            var org = new Organization { Name = organizationName };
            _context.Organizations.Add(org);
            await _context.SaveChangesAsync();

            return RedirectToPage();


        }
    }
}

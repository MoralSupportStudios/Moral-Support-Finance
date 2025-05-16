using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Web.Services;

namespace MoralSupport.Finance.Web.Pages.Organizations
{
    public class CreateModel : PageModel
    {
        private readonly Infrastructure.Persistence.AppDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateModel(Infrastructure.Persistence.AppDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Organization Organization { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Save new organization
            _context.Organizations.Add(Organization);
            await _context.SaveChangesAsync();

            // Link current user to this org
            var userOrg = new UserOrganization
            {
                UserId = _currentUser.UserId,
                OrganizationId = Organization.Id,
                Role = "Admin" // or default role logic
            };
            _context.UserOrganizations.Add(userOrg);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}

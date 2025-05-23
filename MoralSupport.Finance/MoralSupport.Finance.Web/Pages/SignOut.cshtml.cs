using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

namespace MoralSupport.Finance.Web.Pages
{
    public class SignOutModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync()
        {
            await HttpContext.SignOutAsync("MyCookieSchema");
            return RedirectToPage("/Index");
        }
    }
}

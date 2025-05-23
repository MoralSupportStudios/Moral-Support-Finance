using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoralSupport.Authentication.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace MoralSupport.Finance.Web.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly IAuthService _authService;

        public CallbackModel(IAuthService authService)
        {
            _authService = authService;
        }

        [TempData]
        public string? Message { get; set; }

        public async Task<IActionResult> OnGetAsync(string id_token)
        {
            if (string.IsNullOrEmpty(id_token))
            {
                TempData["Message"] = "No id_token received.";
                return Page();
            }

            var user = await _authService.AuthenticateWithGoogleAsync(id_token);
            TempData["Message"] = $"Welcome, {user.Name}!";


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(claims, "MyCookieSchema");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieSchema", principal);
            return RedirectToPage("/Index");


        }
    }
}

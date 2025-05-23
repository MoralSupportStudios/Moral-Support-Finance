using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoralSupport.Authentication.Application.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using MoralSupport.Finance.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;

namespace MoralSupport.Finance.Web.Pages
{
    public class CallbackModel : PageModel
    {
        private readonly IAuthService _authService;
        private readonly AppDbContext _dbContext;
        public CallbackModel(IAuthService authService, AppDbContext dbContext)
        {
            _authService = authService;
            _dbContext = dbContext;

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
            //TempData["Message"] = $"Welcome, {user.Name}!";
            var existingUser = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            {
                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        
                        Name = user.Name,
                        Email = user.Email
                    };
           

                    _dbContext.Users.Add(newUser);
                    await _dbContext.SaveChangesAsync();
                }

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
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;

namespace MoralSupport.Finance.Web.Pages.Organizations.Users
{
    public class IndexModel : PageModel
    {
        private readonly MoralSupport.Finance.Infrastructure.Persistence.AppDbContext _context;

        public IndexModel(MoralSupport.Finance.Infrastructure.Persistence.AppDbContext context)
        {
            _context = context;
        }

        public IList<UserOrganization> UserOrganization { get;set; } = default!;

        public async Task OnGetAsync()
        {
            UserOrganization = await _context.UserOrganizations
                .Include(u => u.Organization)
                .Include(u => u.User).ToListAsync();
        }
    }
}

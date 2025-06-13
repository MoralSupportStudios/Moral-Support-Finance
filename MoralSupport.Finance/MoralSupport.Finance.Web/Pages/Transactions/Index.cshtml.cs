using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MoralSupport.Finance.Domain.Entities;
using MoralSupport.Finance.Infrastructure.Persistence;

namespace MoralSupport.Finance.Web.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly MoralSupport.Finance.Infrastructure.Persistence.AppDbContext _context;

        public IndexModel(MoralSupport.Finance.Infrastructure.Persistence.AppDbContext context)
        {
            _context = context;
        }

        public IList<Transaction> Transactions { get;set; } = default!;
        public List<Account> Accounts { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? AccountId { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? DateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? DateTo { get; set; }


        [BindProperty(SupportsGet = true)]
        public bool? IsExpense { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CheckNumber { get; set; }


        public async Task OnGetAsync()
        {
            Accounts = await _context.Accounts.ToListAsync();
            var query = _context.Transactions
                .Include(t => t.Account)
                .Include(t => t.Payee)
                .Include(t => t.Property)
                .Include(t => t.Category)
                .AsQueryable();
            if (AccountId.HasValue)
                query = query.Where(t => t.AccountId == AccountId);

            if (DateFrom.HasValue)
            {
                query = query.Where(t => t.TransactionDate >= DateFrom.Value);
            }

            if (DateTo.HasValue)
            {
                query = query.Where(t => t.TransactionDate <= DateTo.Value);
            }

            if (IsExpense.HasValue)
                query = query.Where(t => t.IsExpense == IsExpense.Value);

            if (!string.IsNullOrEmpty(CheckNumber))
                query = query.Where(t => t.Description!.Contains(CheckNumber));

            Transactions = await query.ToListAsync();
        }
    }
}

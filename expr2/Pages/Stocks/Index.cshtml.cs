using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;

namespace expr2.Pages.Stocks
{
    public class IndexModel : PageModel
    {
        private readonly expr2.Pages.Models.ExprContext _context;

        public IndexModel(expr2.Pages.Models.ExprContext context)
        {
            _context = context;
        }

        public IList<Stock> Stock { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Stock = await _context.Stocks
                .Include(s => s.Item).ToListAsync();
        }
    }
}

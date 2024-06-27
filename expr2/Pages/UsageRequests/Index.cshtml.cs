using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;

namespace expr2.Pages.UsageRequests
{
    public class IndexModel : PageModel
    {
        private readonly expr2.Pages.Models.ExprContext _context;

        public IndexModel(expr2.Pages.Models.ExprContext context)
        {
            _context = context;
        }

        public IList<UsageRequest> UsageRequest { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string statusFilter { get; set; }
        public async Task OnGetAsync()
        {
            var query = _context.UsageRequests.AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(u => u.Status == statusFilter);
            }
            else
            {
                query = query.Where(u => u.Status == "申请");
            }

            UsageRequest = await query.Include(u => u.Item).ToListAsync();
        }
        public async Task<IActionResult> OnPostAsync(int requestId)
        {
            var usageRequest = await _context.UsageRequests.FindAsync(requestId);
            if (usageRequest == null)
            {
                return NotFound();
            }

            var query = _context.UsageRequests.AsQueryable().Where(u => u.Status == "申请");

            UsageRequest = await query.Include(u => u.Item).ToListAsync();

            // 找到对应的 Item 对象
            var item = await _context.Items.FindAsync(usageRequest.ItemId);
            if (item.Amount>= usageRequest.Quantity)
            {
                // 减去对应物品的库存数量
                item.Amount -= usageRequest.Quantity.GetValueOrDefault();
            }
            else {                 // 库存不足
                TempData["Message"] = 0;
                return Page();
            }
            // 更新领用的状态
            usageRequest.Status = "确认";
            TempData["Message"] = 1;
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

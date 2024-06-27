using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using expr2.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace expr2.Pages.UsageRequests
{
    public class CreateModel : PageModel
    {
        private readonly expr2.Pages.Models.ExprContext _context;

        public CreateModel(expr2.Pages.Models.ExprContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return Page();
        }

        [BindProperty]
        public UsageRequest UsageRequest { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            UsageRequest.Status = "申请";
            var item=await _context.Items.FirstOrDefaultAsync(i=>i.ItemId== UsageRequest.ItemId);
            if(UsageRequest.Quantity > item.Amount)
            {
                ModelState.AddModelError("UsageRequest.Quantity", "领用数量不能大于库存数量");
                return Page();
            }
            var now = DateTime.Now;
            var random = new Random();
            UsageRequest.RequestId = int.Parse($"{now:MMddHHmm}{random.Next(10,99)}");
            UsageRequest.RequestDate = now;
            _context.UsageRequests.Add(UsageRequest);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using expr2.Pages.Models;

namespace expr2.Pages.Stocks
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
        public Stock Stock { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if(Stock.TotalPrice == null)
            {
                Stock.TotalPrice = Stock.PurchaseQuantity * Stock.UnitPrice;
            }
            _context.Stocks.Add(Stock);
            var item = _context.Items.FirstOrDefault(i => i.ItemId == Stock.ItemId);
            if (item != null)
            {
                // 增加数量
                item.Amount += Stock.PurchaseQuantity.GetValueOrDefault();
            }
            var now = DateTime.Now;
            var random = new Random();
            Stock.StockId = int.Parse($"{now:MMddHHmm}{random.Next(10, 99)}");
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

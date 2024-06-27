using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace expr2.Pages.Items
{
    public class IndexModel : PageModel
    {
        private readonly expr2.Pages.Models.ExprContext _context;
        private readonly IConfiguration Configuration;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public string CurrentFilter { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CategoryId
        {
            get; set;
        }

        public IndexModel(expr2.Pages.Models.ExprContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<Item> Item { get; set; }

        public async Task OnGetAsync( int? pageIndex)
        {
            if (SearchString != null)
            {
                pageIndex = 1;
            }
            IQueryable<string> genreQuery = from m in _context.Items
                                            orderby m.CategoryId
                                            select m.CategoryId;
            var items = from m in _context.Items
                         select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => (!string.IsNullOrEmpty(s.ItemName) && s.ItemName.Contains(SearchString))
                || s.ItemId.ToString().Contains(SearchString)
                ||(!string.IsNullOrEmpty(s.CategoryId)&&s.CategoryId.Contains(SearchString))
                || (!string.IsNullOrEmpty(s.Origin) && s.Origin.Contains(SearchString))
                || (!string.IsNullOrEmpty(s.Specification) && s.Specification.Contains(SearchString))
                || (!string.IsNullOrEmpty(s.Model) && s.Model.Contains(SearchString))
                );
            }
            if (!string.IsNullOrEmpty(CategoryId))
            {
                items = items.Where(x => x.CategoryId == CategoryId);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            var pageSize = Configuration.GetValue("PageSize", 4);
            Item = await PaginatedList<Item>.CreateAsync(
                items.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}

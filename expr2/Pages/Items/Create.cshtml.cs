using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using expr2.Pages.Models;

namespace expr2.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly expr2.Pages.Models.ExprContext _context;
        [BindProperty]
        public IFormFile UploadedImage { get; set; }
        public List<SelectListItem> Categories = new List<SelectListItem>
    {
        new SelectListItem { Value = "纸张", Text = "纸张" },
        new SelectListItem { Value = "文具", Text = "文具" },
        new SelectListItem { Value = "刀具", Text = "刀具" },
        new SelectListItem { Value = "单据", Text = "单据" },
        new SelectListItem { Value = "礼品", Text = "礼品" },
        new SelectListItem { Value = "其他", Text = "其他" }
    };
        public List<SelectListItem> Origins = new List<SelectListItem>
    {
        new SelectListItem { Value = "江苏", Text = "江苏" },
        new SelectListItem { Value = "福建", Text = "福建" },
        new SelectListItem { Value = "河南", Text = "河南" },
        new SelectListItem { Value = "河北", Text = "河北" },
        new SelectListItem { Value = "湖南", Text = "湖南" },
        new SelectListItem { Value = "湖北", Text = "湖北" },
        new SelectListItem { Value = "山西", Text = "山西" },
        new SelectListItem { Value = "山东", Text = "山东" },
        new SelectListItem { Value = "安徽", Text = "安徽" },
        new SelectListItem { Value = "江西", Text = "江西" },
        new SelectListItem { Value = "宁夏", Text = "宁夏" },
        new SelectListItem { Value = "四川", Text = "四川" },
        new SelectListItem { Value = "辽宁", Text = "辽宁" },
        new SelectListItem { Value = "吉林", Text = "吉林" },
        new SelectListItem { Value = "黑龙江", Text = "黑龙江" },
        new SelectListItem { Value = "浙江", Text = "浙江" },
        new SelectListItem { Value = "广东", Text = "广东" },
        new SelectListItem { Value = "海南", Text = "海南" },
        new SelectListItem { Value = "贵州", Text = "贵州" },
        new SelectListItem { Value = "云南", Text = "云南" },
        new SelectListItem { Value = "陕西", Text = "陕西" },
        new SelectListItem { Value = "甘肃", Text = "甘肃" },
        new SelectListItem { Value = "青海", Text = "青海" },
        new SelectListItem { Value = "台湾", Text = "台湾" },
        new SelectListItem { Value = "内蒙古", Text = "内蒙古" },
        new SelectListItem { Value = "广西", Text = "广西" },
        new SelectListItem { Value = "西藏", Text = "西藏" },
        new SelectListItem { Value = "新疆", Text = "新疆" },
        new SelectListItem { Value = "北京", Text = "北京" },
        new SelectListItem { Value = "天津", Text = "天津" },
        new SelectListItem { Value = "上海", Text = "上海" },
        new SelectListItem { Value = "重庆", Text = "重庆" },
        new SelectListItem { Value = "香港", Text = "香港" },
        new SelectListItem { Value = "澳门", Text = "澳门" },
    };
        private readonly IWebHostEnvironment _hostingEnvironment;
        public CreateModel(expr2.Pages.Models.ExprContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return 
                  Guid.NewGuid().ToString()
                  + Path.GetExtension(fileName);
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (UploadedImage != null)
            {
                var fileName = GetUniqueFileName(UploadedImage.FileName);
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }
                var filePath = Path.Combine(uploads, fileName);
                Item.ItemImage = fileName;  // Set the file name
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }
            Item.ItemId = _context.Items.Max(i => i.ItemId) + 1;
            Item.Amount = 0;
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}

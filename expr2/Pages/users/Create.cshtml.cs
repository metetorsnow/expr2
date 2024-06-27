using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using expr2.Pages.Models;
using System.Text.Json;

namespace expr2.Pages.users
{
    public class CreateModel : PageModel
    {
        private string _filePath;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _filePath = Path.Combine(_hostingEnvironment.WebRootPath, "user.json");
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public user user { get; set; } = default!;
        private async Task<List<user>> ReadUsersFromFileAsync()
        {
            using (var jsonFileReader = System.IO.File.OpenText(_filePath))
            {
                var usersJson = await jsonFileReader.ReadToEndAsync();
                return JsonSerializer.Deserialize<List<user>>(usersJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<user>();
            }
        }
        private async Task WriteUsersToFileAsync(List<user> users)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var usersJson = JsonSerializer.Serialize(users, options);
            await System.IO.File.WriteAllTextAsync(_filePath, usersJson);
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await ReadUsersFromFileAsync();
            if (users.Any(u => u.userID == user.userID))
            {
                ModelState.AddModelError("user.userID", "用户ID已存在");
                return Page();
            }
            user.type = "user"; // 设置用户类型
            users.Add(user); // 添加新用户
            await WriteUsersToFileAsync(users);

            return RedirectToPage("./Index");
        }
    }
}

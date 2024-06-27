using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;
using System.Text.Json;
using System.IO;

namespace expr2.Pages.users
{
    public class EditModel : PageModel
    {
        private string _filePath;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public EditModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _filePath=Path.Combine(_hostingEnvironment.WebRootPath, "user.json");
        }

        [BindProperty]
        public user User { get; set; } = default!;
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
        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var users = await ReadUsersFromFileAsync();
            User = users.FirstOrDefault(m => m.userID == id);

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var users = await ReadUsersFromFileAsync();
            var userIndex = users.FindIndex(u => u.userID == User.userID);
            if (userIndex != -1)
            {
                User.type = "user"; // 设置用户类型
                users[userIndex] = User; // 更新用户信息
                await WriteUsersToFileAsync(users);
            }
            else
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }

    }
}

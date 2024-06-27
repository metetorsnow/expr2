using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;
using System.Text.Json;

namespace expr2.Pages.users
{
    public class DeleteModel : PageModel
    {
        private string _filePath;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public DeleteModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _filePath = Path.Combine(_hostingEnvironment.WebRootPath, "user.json");
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await ReadUsersFromFileAsync();
            var userToDelete = users.FirstOrDefault(u => u.userID == id);
            if (userToDelete != null)
            {
                users.Remove(userToDelete);
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

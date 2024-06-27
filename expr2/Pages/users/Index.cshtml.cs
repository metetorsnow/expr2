using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;
using System.IO;
using System.Text.Json;

namespace expr2.Pages.users
{
    public class IndexModel : PageModel
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public IndexModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IList<user> user { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var usersPath = Path.Combine(_hostingEnvironment.WebRootPath, "user.json");
            using (var jsonFileReader = System.IO.File.OpenText(usersPath))
            {
                var usersJson = await jsonFileReader.ReadToEndAsync();
                var users = JsonSerializer.Deserialize<List<user>>(usersJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                user = users?.Where(u => u.type == "user").ToList() ?? new List<user>();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using expr2.Pages.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace expr2.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "用户名不能为空")]
        public string Username { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "密码不能为空！")]
        public string Password { get; set; }
        public string Message { get; set; }
        user user = new user();
        
        private readonly IWebHostEnvironment _hostingEnvironment;

        public loginModel(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usersPath = Path.Combine(_hostingEnvironment.WebRootPath, "user.json");
            var usersJson = System.IO.File.ReadAllText(usersPath);
            var users = JsonSerializer.Deserialize<List<user>>(usersJson);

            var user = users?.FirstOrDefault(x => x.userID == Username && x.password == Password);

            if (user != null)
            {
                // 根据用户类型重定向到不同页面
                if (user.type == "admin")
                {
                    //登录
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.userID),
                        new Claim(ClaimTypes.Role, user.type)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToPage("/Index");
                }
                else if (user.type == "user")
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.userID),
                        new Claim(ClaimTypes.Role, user.type)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(5)
                    };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
                    return RedirectToPage("/Index");
                }
            }

            Message = "用户名或密码错误";
            return Page();
        }
    }
}

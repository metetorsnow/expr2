using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace expr2.Pages.Models
{
    public class user
    {
        [Key]
        [Display(Name = "用户ID")]
        [Required(ErrorMessage = "请输入用户ID")]
        public string userID { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string password { get; set; }
        [Display(Name = "用户名")]
        public string? name { get; set; }
        [Display(Name = "性别")]
        public string? gender { get; set; }
        [Display(Name = "出生日期")]
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }
        [Display(Name = "手机号")]
        public string? phone { get; set; }
        public string? type { get; set; }
    }
}

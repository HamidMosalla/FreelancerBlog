using System.ComponentModel.DataAnnotations;

namespace WebFor.Web.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "پر کردن فیلد ایمیل اجباری میباشد.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد پسورد اجباری میباشد.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "به خاطر داشتن؟")]
        public bool RememberMe { get; set; }
    }
}

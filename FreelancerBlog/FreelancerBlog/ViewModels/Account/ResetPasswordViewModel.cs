using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.Web.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="پر کردن فیلد ایمیل اجباری میباشد.")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد پسورد اجباری میباشد.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید پسورد")]
        [Compare("Password", ErrorMessage = "پسورد و تایید پسورد با هم همخوانی ندارند.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}

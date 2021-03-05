using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.Web.ViewModels.Manage
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="پر کردن فیلد پسورد فعلی اجباری است.")]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد فعلی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد پسورد جدید اجباری است.")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "پسورد جدید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "تایید پسورد جدید")]
        [Compare("NewPassword", ErrorMessage = "پسورد جدید و تایید پسورد جدید با هم همخوانی ندارند.")]
        public string ConfirmPassword { get; set; }
    }
}

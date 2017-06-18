using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage ="پر کردن فیلد ایمیل اجباری میباشد.")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید.")]
        public string Email { get; set; }
    }
}

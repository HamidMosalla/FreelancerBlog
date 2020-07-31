using System;
using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.Web.ViewModels.Account
{
    public class ExternalLoginConfirmationViewModel
    {
        public ExternalLoginConfirmationViewModel()
        {
            this.UserRegisteredDate = DateTime.Now;
        }

        [Required(ErrorMessage = "پر کردن فیلد ایمیل اجباری میباشد.")]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "پر کردن فیلد نام و نام خانوادگی اجباری میباشد.")]
        [Display(Name = "نام و نام خانوادگی")]
        [MaxLength(70, ErrorMessage = "مقدار فیلد نمی تواند از 70 کراکتر بیشتر باشد.")]
        public string UserFullName { get; set; }

        [Required(ErrorMessage = "پر کردن بخش جنیست اجباری میباشد.")]
        [Display(Name = "جنسیت")]
        [MaxLength(70, ErrorMessage = "مقدار فیلد نمی تواند از 70 کراکتر بیشتر باشد.")]
        public string UserGender { get; set; }

        public DateTime UserRegisteredDate { get; set; }

        [Required(ErrorMessage = "پر کردن بخش طریقه آشنایی اجباری میباشد.")]
        [Display(Name = "طریقه آشنایی")]
        [MaxLength(70, ErrorMessage = "مقدار فیلد نمی تواند از 70 کراکتر بیشتر باشد.")]
        public string UserHowFindUs { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "برای عضویت در سایت موافقیت با قوانین اجباری میباشد.")]
        public bool TermsAndConditions { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace FreelancerBlog.ViewModels.Contact
{
    public class ContactViewModel
    {
        public ContactViewModel()
        {
            this.ContactDate = DateTime.Now;
        }
        public int ContactId { get; set; }

        [Display(Name = "تاریخ تماس")]
        public DateTime ContactDate { get; set; }

        [Required(ErrorMessage ="پر کردن این فیلد الزامی میباشد.")]
        [MaxLength(100, ErrorMessage ="حداکثر تعداد کراکتر برای این فیلد نمیتواند بیشتر از صد باشد.")]
        [Display(Name ="نام تماس گیرنده")]
        public string ContactName { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        [EmailAddress(ErrorMessage ="لطفا یک ایمیل معتبر وارد کنید.")]
        [MaxLength(100, ErrorMessage = "حداکثر تعداد کراکتر برای این فیلد نمیتواند بیشتر از صد باشد.")]
        [Display(Name = "ایمیل تماس گیرنده")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        [MaxLength(4000, ErrorMessage = "حداکثر تعداد کراکتر برای این فیلد نمیتواند بیشتر از چهار هزار باشد.")]
        [Display(Name = "متن تماس")]
        public string ContactBody { get; set; }

        [Range(1000000000, double.MaxValue, ErrorMessage = "لطفا یک شماره تلفن معتبر وارد کنید.")]
        [Display(Name = "تلفن تماس گیرنده")]
        public string ContactPhone { get; set; }
    }
}

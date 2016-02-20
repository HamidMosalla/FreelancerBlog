using System;
using System.ComponentModel.DataAnnotations;

namespace WebFor.Web.Areas.Admin.ViewModels.Article
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }

        [Required(ErrorMessage ="پر کردن این فیلد اجباری می باشد.")]
        [Display(Name ="عنوان مقاله")]
        [MaxLength(70, ErrorMessage ="عنوان مقاله بیشتر از هفتاد کراکتر نمی تواند باشد.")]
        public string ArticleTitle { get; set; }


        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [Display(Name = "خلاصه مقاله")]
        [MaxLength(400, ErrorMessage = "خلاصه مقاله بیشتر از چهارصد کراکتر نمی تواند باشد.")]
        public string ArticleSummary { get; set; }


        [Display(Name = "تاریخ ایجاد")]
        public DateTime ArticleDateCreated { get; set; }

        [Display(Name = "آخرین تاریخ ویرایش")]
        public DateTime? ArticleDateModified { get; set; }



        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [Display(Name = "متن مقاله")]
        [DataType(DataType.MultilineText)]
        public string ArticleBody { get; set; }


        [Required(ErrorMessage = "انتخاب یک مورد اجباری میباشد.")]
        [Display(Name = "وضعیت مقاله")]
        public string ArticleStatus { get; set; }


        [Display(Name = "برچسب های مقاله")]
        public string ArticleTags { get; set; }


        public string UserIDfk { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Http;

namespace WebFor.Web.Areas.Admin.ViewModels.Portfolio
{
    public class PortfolioViewModelEdit
    {

        public int PortfolioId { get; set; }


        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [MaxLength(70, ErrorMessage = "عنوان پورتفولیو بیشتر از هفتاد کراکتر نمی تواند باشد.")]
        public string PortfolioTitle { get; set; }


        [Display(Name = "تاریخ اتمام پروژه")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public DateTime PortfolioDateBuilt { get; set; }

        [Display(Name = "عکس")]
        public IFormFile PortfolioThumbnailFile { get; set; }

        public string CurrentThumbnail { get; set; }

        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public string PortfolioBody { get; set; }



        [Display(Name = "آدرس سایت")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [DataType(DataType.Url)]
        public string PortfolioSiteAddress { get; set; }



        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public string PortfolioCategory { get; set; }
    }
}
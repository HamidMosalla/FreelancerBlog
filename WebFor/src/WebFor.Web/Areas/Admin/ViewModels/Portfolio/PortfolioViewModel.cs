using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace WebFor.Web.Areas.Admin.ViewModels.Portfolio
{
    public class PortfolioViewModel
    {

        public PortfolioViewModel()
        {
            this.PortfolioDateCreated = DateTime.Now;
        }

        public int PortfolioId { get; set; }


        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [MaxLength(70, ErrorMessage = "عنوان پورتفولیو بیشتر از هفتاد کراکتر نمی تواند باشد.")]
        public string PortfolioTitle { get; set; }



        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public DateTime PortfolioDateCreated { get; set; }



        [Display(Name = "تاریخ اتمام پروژه")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public DateTime PortfolioDateBuilt { get; set; }

        public string PortfolioThumbnail { get; set; }


        [Display(Name = "عکس")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        public IFormFile PortfolioThumbnailFile { get; set; }



        [Display(Name = "توضیحات")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public string PortfolioBody { get; set; }



        [Display(Name = "آدرس سایت")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        [DataType(DataType.Url)]
        public string PortfolioSiteAddress { get; set; }

        [Display(Name = "دسته بندی")]
        public string PortfolioCategory { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "پر کردن این فیلد اجباری می باشد.")]
        public List<string> PortfolioCategoryList { get; set; }
    }
}
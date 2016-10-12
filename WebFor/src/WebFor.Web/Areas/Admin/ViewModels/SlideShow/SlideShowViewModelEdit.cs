using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebFor.Web.Areas.Admin.ViewModels.SlideShow
{
    public class SlideShowViewModelEdit
    {
        public int SlideShowId { get; set; }


        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        public int? SlideShowPriority { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        public string SlideShowTitle { get; set; }


        [Display(Name = "متن")]
        [Required(ErrorMessage = "پر کردن این فیلد الزامی میباشد.")]
        public string SlideShowBody { get; set; }

        [Display(Name = "عکس")]
        public IFormFile SlideShowPictrureFile { get; set; }

        [Display(Name = "لینک")]
        public string SlideShowLink { get; set; }
    }
}

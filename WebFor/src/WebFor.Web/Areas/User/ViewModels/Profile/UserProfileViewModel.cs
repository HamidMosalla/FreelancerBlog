using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;

namespace WebFor.Web.Areas.User.ViewModels.Profile
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage ="پر کردن نام و نام خانوادگی اجباری میباشد.")]
        [Display(Name ="نام و نام خانوادگی")]
        [MaxLength(92, ErrorMessage = "مقدار فیلد نمی تواند از 92 کراکتر بیشتر باشد.")]
        public string UserFullName { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(200, ErrorMessage = "مقدار فیلد نمی تواند از 200 کراکتر بیشتر باشد.")]
        public string UserAddress { get; set; }


        public string UserAvatar { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile UserAvatarFile { get; set; }


        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        [MaxLength(500, ErrorMessage = "مقدار فیلد نمی تواند از 500 کراکتر بیشتر باشد.")]
        public string UserBio { get; set; }


        [Required(ErrorMessage = "انتخاب جنسیت اجباری میباشد.")]
        public string UserGender { get; set; }


        [Display(Name = "شغل")]
        [MaxLength(200, ErrorMessage = "مقدار فیلد نمی تواند از 200 کراکتر بیشتر باشد.")]
        public string UserOccupation { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "وب سایت")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserWebSite { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "پروفایل گوگل")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserGoogleProfile { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "پروفایل فیس بوک")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserFacebookProfile { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "پروفایل تویتر")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserTwitterProfile { get; set; }


        [DataType(DataType.Url)]
        [Display(Name = "پروفایل لینکدین")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserLinkedInProfile { get; set; }


        [Display(Name = "تخصص ها")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserSpeciality { get; set; }


        [Display(Name = "علاقه مندی ها")]
        [MaxLength(400, ErrorMessage = "مقدار فیلد نمی تواند از 400 کراکتر بیشتر باشد.")]
        public string UserFavourites { get; set; }

        [Display(Name = "تاریخ تولد")]
        public DateTime? UserDateOfBirth { get; set; }
    }
}

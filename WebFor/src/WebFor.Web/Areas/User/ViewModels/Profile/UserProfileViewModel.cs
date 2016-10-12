using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebFor.Core.Domain;
using WebFor.Web.Resources;

namespace WebFor.Web.Areas.User.ViewModels.Profile
{
    public class UserProfileViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "UserFullNameRequired")]
        [Display(Name ="FullName", ResourceType = typeof(Resources.DataAnnotations))]
        [MaxLength(92, ErrorMessage = "MaxLengthValidation")]
        public string UserFullName { get; set; }


        [Display(Name = "آدرس")]
        [MaxLength(200, ErrorMessage = "مقدار فیلد نمی تواند از 200 کراکتر بیشتر باشد.")]
        public string UserAddress { get; set; }


        public string UserAvatar { get; set; }

        [Display(Name = "آواتار")]
        public IFormFile UserAvatarFile { get; set; }


        [Display(Name = "درباره")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1000, ErrorMessage = "مقدار فیلد نمی تواند از 500 کراکتر بیشتر باشد.")]
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

        [Display(Name = "شماره تلفن")]
        [Range(10000000, 100000000000, ErrorMessage = "لطفا یک شماره تلفن معتبر وارد کنید.")]
        [MaxLength(12, ErrorMessage = "مقدار فیلد نمی تواند از 12 کراکتر بیشتر باشد.")]
        public string UserPhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [EmailAddress(ErrorMessage = "لطفا یک ایمیل معتبر وارد کنید.")]
        [MaxLength(100, ErrorMessage = "مقدار فیلد نمی تواند از 100 کراکتر بیشتر باشد.")]
        public string UserProfileEmail { get; set; }



        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
    }
}

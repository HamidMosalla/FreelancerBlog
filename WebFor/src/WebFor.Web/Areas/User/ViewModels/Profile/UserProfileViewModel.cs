using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFor.Web.Areas.User.ViewModels.Profile
{
    public class UserProfileViewModel
    {

        [Required(ErrorMessage ="پر کردن نام و نام خانوادگی اجباری میباشد.")]
        public string UserFullName { get; set; }

        public string UserAddress { get; set; }

        public string UserAvatar { get; set; }

        public string UserBio { get; set; }


        [Required(ErrorMessage = "انتخاب جنسیت اجباری میباشد.")]
        public string UserGender { get; set; }

        public string UserOccupation { get; set; }

        public string UserWebSite { get; set; }

        public string UserGoogleProfile { get; set; }

        public string UserFacebookProfile { get; set; }

        public string UserTwitterProfile { get; set; }

        public string UserLinkedInProfile { get; set; }

        public string UserSpeciality { get; set; }

        public string UserFavourites { get; set; }

        public DateTime? UserDateOfBirth { get; set; }
    }
}

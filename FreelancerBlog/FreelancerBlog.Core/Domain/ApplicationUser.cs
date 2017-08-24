using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FreelancerBlog.Core.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserFullName { get; set; }
        public string UserAddress { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserProfileEmail { get; set; }
        public string UserAvatar { get; set; }
        public string UserBio { get; set; }
        public string UserGender { get; set; }
        public string UserOccupation { get; set; }
        public string UserWebSite { get; set; }
        public string UserGoogleProfile { get; set; }
        public string UserFacebookProfile { get; set; }
        public string UserTwitterProfile { get; set; }
        public string UserLinkedInProfile { get; set; }
        public int? UserPoints { get; set; }
        public string UserSpeciality { get; set; }
        public string UserFavourites { get; set; }
        public DateTime UserRegisteredDate { get; set; }
        public DateTime? UserBanEndDate { get; set; }
        public DateTime? UserDateOfBirth { get; set; }
        public string UserHowFindUs { get; set; }


        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ArticleRating> ArticleRatings { get; set; }
        public virtual ICollection<ArticleComment> ArticleComments { get; set; }
    }
}

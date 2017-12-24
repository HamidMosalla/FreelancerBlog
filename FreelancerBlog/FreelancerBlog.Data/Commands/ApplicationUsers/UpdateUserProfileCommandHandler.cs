using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreelancerBlog.Core.Commands.Data.ApplicationUsers;
using FreelancerBlog.Data.EntityFramework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Data.Commands.ApplicationUsers
{
    public class UpdateUserProfileCommandHandler : AsyncRequestHandler<UpdateUserProfileCommand>
    {
        private FreelancerBlogContext _context;

        public UpdateUserProfileCommandHandler(FreelancerBlogContext context)
        {
            _context = context;
        }

        protected override Task HandleCore(UpdateUserProfileCommand message)
        {
            var model = _context.Users.Single(u => u.Id == message.ApplicationUser.Id);

            model.UserAddress = message.ApplicationUser.UserAddress;
            model.UserProfileEmail = message.ApplicationUser.UserProfileEmail;
            model.UserPhoneNumber = message.ApplicationUser.UserPhoneNumber;
            model.UserAvatar = message.ApplicationUser.UserAvatar ?? model.UserAvatar;
            model.UserBio = message.ApplicationUser.UserBio;
            model.UserDateOfBirth = message.ApplicationUser.UserDateOfBirth;
            model.UserFacebookProfile = message.ApplicationUser.UserFacebookProfile;
            model.UserFavourites = message.ApplicationUser.UserFavourites;
            model.UserFullName = message.ApplicationUser.UserFullName;
            model.UserGender = message.ApplicationUser.UserGender;
            model.UserGoogleProfile = message.ApplicationUser.UserGoogleProfile;
            model.UserHowFindUs = message.ApplicationUser.UserHowFindUs;
            model.UserLinkedInProfile = message.ApplicationUser.UserLinkedInProfile;
            model.UserOccupation = message.ApplicationUser.UserOccupation;
            model.UserSpeciality = message.ApplicationUser.UserSpeciality;
            model.UserTwitterProfile = message.ApplicationUser.UserTwitterProfile;
            model.UserWebSite = message.ApplicationUser.UserWebSite;

            return _context.SaveChangesAsync();
        }
    }
}
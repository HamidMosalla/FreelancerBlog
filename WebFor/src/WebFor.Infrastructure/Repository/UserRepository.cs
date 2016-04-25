using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebFor.Core.Domain;
using WebFor.Core.Repository;
using WebFor.Infrastructure.EntityFramework;

namespace WebFor.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private WebForDbContext _context;

        public UserRepository(WebForDbContext context)
        {
            _context = context;
        }
        public void Add(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser entity)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> FindByIdAsync(string id)
        {
            return _context.Users.SingleAsync(u => u.Id.Equals(id));

            //var userStore = new UserStore<ApplicationUser>(_context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user = userManager.FindById(User.Identity.GetUserId());
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUser>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateUserProfileAsync(ApplicationUser user)
        {
            //_context.Users.Attach(user, GraphBehavior.SingleObject);

            //var entity = _context.Entry(user);

            //entity.Property(e => e.UserAddress).IsModified = true;
            //entity.Property(e => e.UserAvatar).IsModified = true;
            //entity.Property(e => e.UserBio).IsModified = true;
            //entity.Property(e => e.UserDateOfBirth).IsModified = true;
            //entity.Property(e => e.UserFacebookProfile).IsModified = true;
            //entity.Property(e => e.UserFavourites).IsModified = true;
            //entity.Property(e => e.UserFullName).IsModified = true;
            //entity.Property(e => e.UserGender).IsModified = true;
            //entity.Property(e => e.UserGoogleProfile).IsModified = true;
            //entity.Property(e => e.UserHowFindUs).IsModified = true;
            //entity.Property(e => e.UserLinkedInProfile).IsModified = true;
            //entity.Property(e => e.UserOccupation).IsModified = true;
            //entity.Property(e => e.UserSpeciality).IsModified = true;
            //entity.Property(e => e.UserTwitterProfile).IsModified = true;
            //entity.Property(e => e.UserWebSite).IsModified = true;

            var model = await _context.Users.SingleAsync(u => u.Id.Equals(user.Id));

            model.UserAddress = user.UserAddress;
            model.UserProfileEmail = user.UserProfileEmail;
            model.UserPhoneNumber = user.UserPhoneNumber;
            model.UserAvatar = user.UserAvatar ?? model.UserAvatar;
            model.UserBio = user.UserBio;
            model.UserDateOfBirth = user.UserDateOfBirth;
            model.UserFacebookProfile = user.UserFacebookProfile;
            model.UserFavourites = user.UserFavourites;
            model.UserFullName = user.UserFullName;
            model.UserGender = user.UserGender;
            model.UserGoogleProfile = user.UserGoogleProfile;
            model.UserHowFindUs = user.UserHowFindUs;
            model.UserLinkedInProfile = user.UserLinkedInProfile;
            model.UserOccupation = user.UserOccupation;
            model.UserSpeciality = user.UserSpeciality;
            model.UserTwitterProfile = user.UserTwitterProfile;
            model.UserWebSite = user.UserWebSite;


            return await _context.SaveChangesAsync();
        }

        public void Detach(ApplicationUser model)
        {
            _context.Entry(model).State = EntityState.Detached;
        }

        public Task<ApplicationUser> FindByUserNameAsync(string userName)
        {
            return _context.Users.Include(u => u.Articles).ThenInclude(u => u.ArticleRatings).Include(u => u.ArticleComments).SingleOrDefaultAsync(u => u.Email.Equals(userName));
        }
    }
}

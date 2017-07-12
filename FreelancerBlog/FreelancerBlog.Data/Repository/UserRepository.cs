using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private FreelancerBlogContext _context;

        public UserRepository(FreelancerBlogContext context)
        {
            _context = context;
        }


        public void Add(ApplicationUser entity)
        {
            _context.Users.Add(entity);
        }

        public void Remove(ApplicationUser entity)
        {
            _context.Users.Remove(entity);
        }

        public void Update(ApplicationUser user)
        {
            _context.Users.Attach(user);
           _context.Entry(user).State = EntityState.Modified;
        }

        public Task<ApplicationUser> FindByIdAsync(string id)
        {
            return _context.Users.SingleAsync(u => u.Id.Equals(id));

            //var userStore = new UserStore<ApplicationUser>(_context);
            //var userManager = new UserManager<ApplicationUser>(userStore);
            //var user = userManager.FindById(User.Identity.GetUserId());
        }

        public Task<List<ApplicationUser>> GetAllAsync()
        {
            return _context.Users.ToListAsync();
        }

        public async Task<int> UpdateUserProfileAsync(ApplicationUser user)
        {
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
            return
                _context.Users.Include(u => u.Articles)
                    .ThenInclude(u => u.ArticleRatings)
                    .Include(u => u.ArticleComments)
                    .SingleOrDefaultAsync(u => u.Email.Equals(userName));
        }
    }
}

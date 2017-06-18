using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;

namespace FreelancerBlog.Core.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        Task<int> UpdateUserProfileAsync(ApplicationUser user);
        void Detach(ApplicationUser model);
        Task<ApplicationUser> FindByUserNameAsync(string userName);
    }
}

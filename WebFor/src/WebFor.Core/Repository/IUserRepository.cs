using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IUserRepository : IRepository<ApplicationUser, string>
    {
        Task<int> UpdateUserProfileAsync(ApplicationUser user);
        void Detach(ApplicationUser model);
    }
}

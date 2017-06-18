using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;

namespace FreelancerBlog.Core.Repository
{
    public interface IContactRepository : IRepository<Contact, int>
    {
        Task<int> AddNewContactAsync(Contact contact);
        Task<int> DeleteContactAsync(Contact model);
    }
}

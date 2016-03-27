using System.Threading.Tasks;
using WebFor.Core.Domain;

namespace WebFor.Core.Repository
{
    public interface IContactRepository : IRepository<Contact, int>
    {
        Task<int> AddNewContactAsync(Contact contact);
        Task<int> DeleteContactAsync(Contact model);
    }
}

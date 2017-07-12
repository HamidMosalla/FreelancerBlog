using System.Collections.Generic;
using System.Threading.Tasks;
using FreelancerBlog.Core.Domain;
using FreelancerBlog.Core.Repository;
using FreelancerBlog.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace FreelancerBlog.Infrastructure.Repository
{
    public class ContactRepository:IContactRepository
    {
        private FreelancerBlogContext _context;

        public ContactRepository(FreelancerBlogContext context)
        {
            _context = context;
        }


        public void Add(Contact entity)
        {
            _context.Contacts.Add(entity);
        }

        public void Remove(Contact entity)
        {
            _context.Contacts.Remove(entity);
        }

        public void Update(Contact entity)
        {
            _context.Contacts.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public Task<Contact> FindByIdAsync(int id)
        {
            return _context.Contacts.SingleOrDefaultAsync(c => c.ContactId.Equals(id));
        }

        public Task<List<Contact>> GetAllAsync()
        {
            return _context.Contacts.ToListAsync();
        }

        public Task<int> AddNewContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            return _context.SaveChangesAsync();
        }

        public Task<int> DeleteContactAsync(Contact model)
        {
            _context.Contacts.Remove(model);
            return _context.SaveChangesAsync();
        }
    }
}

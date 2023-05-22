using FirstContactAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace FirstContactAPI.Repository
{
    public class FirstContactRepository : IFirstContactRepository
    {
        public readonly FirstContactContext _context;

        public FirstContactRepository(FirstContactContext context)
        {
            _context = context;
        }

        public async Task<FirstContact> Create(FirstContact firstContact)
        {
            _context.FirstContacts.Add(firstContact);
            await _context.SaveChangesAsync();
            return firstContact;
        }

        public async Task Delete(int Id)
        {
            var contact = await _context.FirstContacts.FindAsync(Id);
            _context.FirstContacts.Remove(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FirstContact>> Get()
        {
            return await _context.FirstContacts.ToListAsync();
        }

        public async Task<FirstContact> Get(int id)
        {
            return await _context.FirstContacts.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task Update(FirstContact firstContact)
        {
            _context.Entry(firstContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}

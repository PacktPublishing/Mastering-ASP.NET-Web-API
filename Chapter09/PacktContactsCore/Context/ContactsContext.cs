using Microsoft.EntityFrameworkCore;
using PacktContactsCore.Model;

namespace PacktContactsCore.Context
{
    public class ContactsContext : DbContext
    {
        public ContactsContext(DbContextOptions<ContactsContext> options)
            : base(options) { }
        public ContactsContext() {            
        }
        public DbSet<Contacts> Contacts { get; set; }
    }
}

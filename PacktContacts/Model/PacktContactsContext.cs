using Microsoft.EntityFrameworkCore;
using PacktContacts.Model;

namespace PacktContacts.Models
{
    public class PacktContactsContext : DbContext
    {
        public PacktContactsContext(DbContextOptions<PacktContactsContext> options)
            :base(options)
        {
        }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}

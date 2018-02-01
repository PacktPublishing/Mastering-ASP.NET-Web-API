using System.ComponentModel.DataAnnotations;

namespace PacktContacts.Model
{
    public class Contacts
    {       
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
    }
}
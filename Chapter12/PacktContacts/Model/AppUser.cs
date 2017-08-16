namespace PacktContacts.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsSuperUser { get; set; }
    }
}

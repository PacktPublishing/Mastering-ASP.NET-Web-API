using System;
using System.ComponentModel.DataAnnotations;

namespace PacktContactsCore.Model
{
    public class Contacts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
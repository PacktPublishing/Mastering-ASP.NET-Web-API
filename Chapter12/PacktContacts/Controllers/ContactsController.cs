using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PacktContacts.Model;
using PacktContacts.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PacktContacts.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly PacktContactsContext _context;        

        public ContactsController(PacktContactsContext context)
        {   
            _context = context;
        }       

        // GET: api/Contacts
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Contacts.ToListAsync());
        }

        // GET api/Contacts/5
        [HttpGet("{id:int}", Name = "GetContacts")]
        public async Task<IActionResult> Get(int id)
        {
            var findContact = await _context.Contacts.FindAsync(id);
            if (findContact != null)
            {
                return Ok(findContact);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/Contacts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Contacts contactsInfo)
        {
            if (contactsInfo == null)
            {
                return BadRequest();
            }
            await _context.Contacts.AddAsync(contactsInfo);
            await Save();
            return CreatedAtRoute("GetContacts", new { Controller = "Contacts", id = contactsInfo.Id }, contactsInfo);
        }

        // PUT api/Contacts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Contacts updateContactsInfo)
        {
            if (updateContactsInfo == null)
            {
                return BadRequest();
            }
            //var contactObj = await _context.Contacts.FindAsync(id);
            var contactObj = _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
            if (contactObj == null)
            {
                return NotFound();
            }            
            contactObj.FirstName = updateContactsInfo.FirstName;
            contactObj.LastName = updateContactsInfo.LastName;
            contactObj.Email = updateContactsInfo.Email;            
            _context.Contacts.Update(contactObj);            
            await _context.SaveChangesAsync();            
            return new NoContentResult();
        }

        // DELETE api/Contacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var itemToRemove = await _context.Contacts.FindAsync(id);
            if (itemToRemove != null)
            {
                _context.Contacts.Remove(itemToRemove);
                await Save();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
        private async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
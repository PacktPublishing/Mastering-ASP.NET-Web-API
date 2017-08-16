using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PacktContactsCore.Model;
using PacktContactsCore.Context;

namespace PacktContactsCore.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsContext _context;
        public ContactsController(ContactsContext contactContext)
        {
            _context = contactContext;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Contacts.ToList());
        }

        // GET api/values/5
        [HttpGet("{id}", Name ="GetContactById")]
        public IActionResult Get(int id)
        {
            var result = _context.Contacts.Any(c => c.Id == id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(_context.Contacts.Where(c => c.Id == id).FirstOrDefault());
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Contacts reqObj)
        {
            if (reqObj == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contextObj = _context.Contacts.Add(reqObj);
            var count = _context.SaveChanges();
            if (count == 1)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Contacts reqObj)
        {
            if (reqObj == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = _context.Contacts.Any(c => c.Id == id);
            if (!result)
            {
                return NotFound();
            }

            var savedrec = _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
            // Use of AutoMapper removes below mapping
            // Showing old school technique
            savedrec.FirstName = reqObj.FirstName;
            savedrec.LastName = reqObj.LastName;
            savedrec.Email = reqObj.Email;
            savedrec.DateOfBirth = reqObj.DateOfBirth;

            _context.Contacts.Update(savedrec);
            var count = _context.SaveChanges();
            if (count == 1)
            {
                return StatusCode(201, "Updated Sucessfully!");
            }
            else
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Contacts.Any(c => c.Id == id);
            if (!result)
            {
                return NotFound();
            }

            var savedrec = _context.Contacts.Where(c => c.Id == id).FirstOrDefault();
            _context.Contacts.Remove(savedrec);
            var count = _context.SaveChanges();
            if (count == 1)
            {
                return NoContent();
            }
            else
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
        }
    }
}

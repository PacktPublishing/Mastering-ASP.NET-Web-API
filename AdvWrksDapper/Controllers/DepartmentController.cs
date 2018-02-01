using Microsoft.AspNetCore.Mvc;
using AdvWrksDapper.Models;
using AdvWrksDapper.Repository;

namespace AdvWrksDapper.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _deptrepo;
        public DepartmentController(IDepartmentRepository deptrepo)
        {
            _deptrepo = deptrepo;
        }
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var results = _deptrepo.GetDepartments();
            return Ok(results);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {            
            if (!_deptrepo.DepartmentExists(id))
            {
                return NotFound();
            }
            var dept = _deptrepo.GetDepartment(id);
            return Ok(dept);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Department dept)
        {
            if (dept == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_deptrepo.AddDepartment(dept))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return StatusCode(201, "Created Successfully");
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Department dept)
        {
            if (dept == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_deptrepo.DepartmentExists(id))
            {
                return NotFound();
            }

            if (!_deptrepo.UpdateDepartment(dept))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return StatusCode(202, "Updated Successfully");
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_deptrepo.DepartmentExists(id))
            {
                return NotFound();
            }
            if(!_deptrepo.DeleteDepartment(id))
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }
            else
            {
                return NoContent();
            }
        }
    }
}

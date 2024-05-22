using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationCaching.Caching;
using WebApplicationCaching.Data;
using WebApplicationCaching.Models;

namespace WebApplicationCaching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeContext _context;
        private readonly ICaching _caching;
        public EmployeeController(ICaching caching, EmployeeContext context)
        {
            _caching = caching;
            _context =  context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var list = _caching.GetData<IEnumerable<EmployeeDetail>>("EmployeeDetails");
            if(list != null && list.Count() > 0 )
            {
                return Ok(list);
            }

            
            var employees = await _context.EmployeeDetails.ToListAsync();
            var expir = DateTimeOffset.Now.AddMinutes(4);
            var x = _caching.SetData(employees,"EmployeeDetails",expir);

            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody]EmployeeDetail employee)
        {
            try
            {
                var x = await _context.EmployeeDetails.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Ok(employee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

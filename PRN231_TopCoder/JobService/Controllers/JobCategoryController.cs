using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JobService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCategoryController : ControllerBase
    {
        private readonly jobServiceContext _context;
        public JobCategoryController(jobServiceContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetByJobId(int id) {
            return Ok(_context.JobCategories.Include(c => c.Category).Where(j => j.JobId == id));    
        }
    }
}

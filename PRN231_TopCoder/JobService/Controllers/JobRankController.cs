using JobService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JobService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobRankController : ControllerBase
    {
        private readonly jobServiceContext _context;
        public JobRankController(jobServiceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.Ranks);
        }

        [HttpGet("{id}")]
        public IActionResult GetByJobId(int id)
        {
            return Ok(_context.JobRanks.Include(r => r.Rank).Where(r => r.JobId == id));   
        }
    }
}

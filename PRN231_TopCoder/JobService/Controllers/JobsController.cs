using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobService.Models;

namespace JobService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly jobServiceContext _context;

        public JobsController(jobServiceContext context)
        {
            _context = context;
        }

        // GET: api/Jobs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            return await _context.Jobs.Where(s => s.IsDelete.Equals(1) && s.Status.Equals("Accept")).ToListAsync();
        }

        [HttpGet("GetJobList")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobList()
        {
            return await _context.Jobs.Where(s => s.IsDelete.Equals(1)).ToListAsync();
        }

        [HttpGet("GetJobByCategory/{id}")]
        public IActionResult GetJobByCategory(int id)
        {
            var job = _context.JobCategories.Include(j => j.Job).Where(i => i.CategoryId == id).ToList();
            return Ok(job);
        }

        [HttpGet("GetJobByRank/{id}")]
        public IActionResult GetJobByRank(int id)
        {
            var job = _context.JobRanks.Include(j => j.Job).Where(i => i.RankId == id).ToList();
            return Ok(job);
        }

        [HttpGet("GetJobByWishList/{id}")]
        public IActionResult GetJobByWishList(int id)
        {
            var job = _context.Wishlists.Include(j => j.Job).Where(i => i.UserId == id).ToList();
            return Ok(job);
        }


       [HttpGet("GetJobByBusinessId/{id}")]
        public ActionResult<IEnumerable<Job>> GetJobByBusinessId(int id)
        {
            return _context.Jobs.Where(b => b.BusinessId == id).Where(s => s.IsDelete.Equals(1)).ToList();
        }

       

        // GET: api/Jobs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return job;
        }

        // PUT: api/Jobs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJob(int id, Job job)
        {
            if (id != job.JobId)
            {
                return BadRequest();
            }

            _context.Entry(job).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Jobs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", new { id = job.JobId }, job);
        }

        // DELETE: api/Jobs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            job.IsDelete = 0;
            _context.Jobs.Update(job);
            await _context.SaveChangesAsync();
            var wishlist = _context.Wishlists.Where(j => j.JobId == id).ToList();
            foreach (var wish in wishlist)
            {
                _context.Wishlists.Remove(wish);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}

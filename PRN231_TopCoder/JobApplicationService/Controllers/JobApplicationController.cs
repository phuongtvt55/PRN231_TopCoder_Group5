using JobApplicationService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace JobApplicationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private readonly jobApplicationServiceContext _dbContext;

        public JobApplicationController(jobApplicationServiceContext dbContext)
        {
            _dbContext = dbContext;
        }


        // get all
        [HttpGet]
        public IActionResult GetAllJobApplications()
        {
            try
            {
                var jobApplications = _dbContext.JobApplications.ToList();
                if (jobApplications.Count() == 0)
                {
                    return NotFound("There is no available Jobs");
                }
                return Ok(jobApplications);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
        }

        // get 1 
        [HttpGet("{id}")]
        public IActionResult GetJobApplication(int id) {
            try
            {
                var jobApplication = _dbContext.JobApplications.Find(id);
                if (jobApplication == null)
                {
                    return NotFound("Cannot find job");
                }
                return Ok(jobApplication);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // post
        [HttpPost]
        public IActionResult PostJobApplication(JobApplication ja)
        {
            try
            {
                _dbContext.Add(ja);
                _dbContext.SaveChanges();
                return Ok("Job application created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //put
        [HttpPut("{id}")]
        public IActionResult PutJobApplication(JobApplication model)
        {
            if (model == null)
            {
                return BadRequest("Invalid data");
            }

            try
            {
                var job = _dbContext.JobApplications.Find(model.ApplicationId);
                if (job == null)
                {
                    return NotFound("Not found with ID");
                }
                job.ApplicationId = model.ApplicationId;
                job.UserId = model.UserId;
                job.JobId = model.JobId;
                job.ApplyDate = model.ApplyDate;
                job.IsDelete = model.IsDelete;
                job.Status = model.Status;
                _dbContext.SaveChanges();
                return Ok("Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        // delete
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var ja = _dbContext.JobApplications.Find(id);
                if (ja == null)
                {
                    return NotFound("Cannot find job with ID");
                }
                _dbContext.JobApplications.Remove(ja);
                _dbContext.SaveChanges();
                return Ok("Job Application Delete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

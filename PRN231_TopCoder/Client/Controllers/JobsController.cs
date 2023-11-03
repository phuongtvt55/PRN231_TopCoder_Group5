﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobService.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace Client.Controllers
{
    public class JobsController : Controller
    {
        private readonly jobServiceContext _context;
        private readonly HttpClient client;
        private string api = "";

        public JobsController()
        {
            _context = new jobServiceContext();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44300/api/Jobs";
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(api);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Job> list = JsonSerializer.Deserialize<List<Job>>(data, options);
            ViewData["Category"]  = _context.Categories.ToList();
            ViewData["Wishlist"] = _context.Wishlists.ToList();
            ViewData["JobCategory"]  = _context.JobCategories.Include(c => c.Category).ToList();
            ViewData["JobRank"] = _context.JobRanks.Include(c => c.Rank).ToList();
            ViewData["UserId"] = 1;
            return View(list);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpResponseMessage response = await client.GetAsync(api + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Job job = JsonSerializer.Deserialize<Job>(data, options);
                return View(job);
            }
            return NotFound();
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["Rank"] = _context.Ranks.ToList();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection form,Job job)
        {
            string[] selectedCategories = Request.Form["category"];
            string[] selectedRanks = Request.Form["rank"];
            string salaryFrom = Request.Form["SalaryFrom"];
            string salaryTo = Request.Form["SalaryTo"];
            job.BusinessId = 1;
            job.PostDate = DateTime.Today;
            job.Salary = salaryFrom + "-" + salaryTo;
            job.IsDelete = 0;
            job.Status = "Waiting";
            string data = JsonSerializer.Serialize(job);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(api, content);
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                Uri location = response.Headers.Location;
                string jobIdString = location.Segments.Last();
                foreach (var item in selectedCategories)
                {
                    JobCategory jobCategory = new JobCategory
                    {
                        JobId = Int32.Parse(jobIdString),
                        CategoryId = Int32.Parse(item),
                    };
                    _context.JobCategories.Add(jobCategory);
                    _context.SaveChanges();
                }

                foreach(var item in selectedRanks)
                {
                    JobRank jobRank = new JobRank
                    {
                        JobId = Int32.Parse(jobIdString),
                        RankId = Int32.Parse(item),
                    };
                    _context.JobRanks.Add(jobRank);
                    _context.SaveChanges();
                }  
                return RedirectToAction(nameof(Index));
            }
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["Rank"] = _context.Ranks.ToList();
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["Rank"] = _context.Ranks.ToList();
            ViewData["JobCategory"] = _context.JobCategories.Where(j => j.JobId == id).ToList();
            ViewData["JobRank"] = _context.JobRanks.Where(j => j.JobId == id).ToList();
            HttpResponseMessage response = await client.GetAsync(api + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Job job = JsonSerializer.Deserialize<Job>(data, options);
                string[] salary = job.Salary.Split("-");
                ViewData["SalaryFrom"] = salary[0];
                ViewData["SalaryTo"] = salary[1];
                return View(job);
            }
            return NotFound();
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormCollection form,int id, Job job)
        {
            if (id != job.JobId)
            {
                return NotFound();
            }
            string[] selectedCategories = Request.Form["category"];
            string[] selectedRanks = Request.Form["rank"];
            string salaryFrom = Request.Form["SalaryFrom"];
            string salaryTo = Request.Form["SalaryTo"];
            job.BusinessId = 1;
            job.PostDate = DateTime.Today;
            job.Salary = salaryFrom + "-" + salaryTo;
            job.IsDelete = 0;
            job.Status = "Waiting";
            string data = JsonSerializer.Serialize(job);
            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(api + "/" + id, content);
            if(response.IsSuccessStatusCode)
            {
                //Remove jobCate and jobRank
                var jobCate = _context.JobCategories.Where(j => j.JobId == job.JobId).ToList();
                var jobRank = _context.JobRanks.Where(j => j.JobId == job.JobId).ToList();
                foreach(var item in jobCate)
                {
                    _context.JobCategories.Remove(item);
                    _context.SaveChanges();
                }
                foreach(var item in jobRank)
                {
                    _context.JobRanks.Remove(item);
                    _context.SaveChanges();
                }

                //Add new jobCate and JobRank
                foreach (var item in selectedCategories)
                {
                    JobCategory jobCategory = new JobCategory
                    {
                        JobId = job.JobId,
                        CategoryId = Int32.Parse(item),
                    };
                    _context.JobCategories.Add(jobCategory);
                    _context.SaveChanges();
                }

                foreach (var item in selectedRanks)
                {
                    JobRank rank = new JobRank
                    {
                        JobId = job.JobId,
                        RankId = Int32.Parse(item),
                    };
                    _context.JobRanks.Add(rank);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewData["Category"] = _context.Categories.ToList();
            ViewData["Rank"] = _context.Ranks.ToList();
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}

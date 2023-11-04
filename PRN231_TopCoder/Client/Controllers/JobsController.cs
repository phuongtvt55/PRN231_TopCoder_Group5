using System;
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
using UserService.Models;
using System.Collections;
using System.IO;

namespace Client.Controllers
{
    public class JobsController : Controller
    {
        private readonly jobServiceContext _context;
        private readonly HttpClient client;
        private string api = "";
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public JobsController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = new jobServiceContext();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44300/api/Jobs";
            _hostingEnvironment = hostingEnvironment;
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
            var userId = HttpContext.Session.GetInt32("UserId");
            var businessId = HttpContext.Session.GetInt32("BussinessId");
            var role = HttpContext.Session.GetString("MyRole");
            //**
            ViewData["UserId"] = 4;
            
            return View(list);
        }

        public async Task<IActionResult> GetJobByCategoryId(int id)
        {
            HttpResponseMessage response = await client.GetAsync(api + "/GetJobByCategory/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<JobCategory> listJobcategory = JsonSerializer.Deserialize<List<JobCategory>>(data, options);
                List<Job> list = new List<Job>();
                foreach(var item in listJobcategory)
                {
                    list.Add(item.Job);
                }
                ViewData["Category"] = _context.Categories.ToList();
                ViewData["Wishlist"] = _context.Wishlists.ToList();
                ViewData["JobCategory"] = _context.JobCategories.Include(c => c.Category).ToList();
                ViewData["JobRank"] = _context.JobRanks.Include(c => c.Rank).ToList();
                //**
                ViewData["UserId"] = 4;
                return View("Index", list);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetJobByRankId(int id) {
            HttpResponseMessage response = await client.GetAsync(api + "/GetJobByRank/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                List<JobRank> listRank = JsonSerializer.Deserialize<List<JobRank>>(data, options);
                List<Job> list = new List<Job>();
                foreach(var item in listRank)
                {
                    list.Add(item.Job);
                }
                ViewData["Category"] = _context.Categories.ToList();
                ViewData["Wishlist"] = _context.Wishlists.ToList();
                ViewData["JobCategory"] = _context.JobCategories.Include(c => c.Category).ToList();
                ViewData["JobRank"] = _context.JobRanks.Include(c => c.Rank).ToList();
                //**
                ViewData["UserId"] = 4;
                return View("Index", list);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetJobByWishListId()
        {
            //**
            var userId = 4;
            HttpResponseMessage response = await client.GetAsync(api + "/GetJobByWishList/" + userId);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                List<Wishlist> wishList = JsonSerializer.Deserialize<List<Wishlist>>(data, options);
                List<Job> list = new List<Job>();
                foreach (var item in wishList)
                {
                    list.Add(item.Job);
                }
                ViewData["Category"] = _context.Categories.ToList();
                ViewData["Wishlist"] = _context.Wishlists.ToList();
                ViewData["JobCategory"] = _context.JobCategories.Include(c => c.Category).ToList();
                ViewData["JobRank"] = _context.JobRanks.Include(c => c.Rank).ToList();
                //**
                ViewData["UserId"] = 4;
                return View("Index", list);
            }
            return NotFound();
        }

        public async Task<IActionResult> MyJobList()
        {
            //**
            var id = 15;
            HttpResponseMessage response = await client.GetAsync(api + "/GetJobByBusinessId/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<Job> list = JsonSerializer.Deserialize<List<Job>>(data, options);
     
                ViewData["JobCategory"] = _context.JobCategories.Include(c => c.Category).ToList();
                ViewData["JobRank"] = _context.JobRanks.Include(c => c.Rank).ToList();

                return View(list);
            }
            return NotFound();
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
                //Get business profile
                var _db = new userServiceContext();
                var business = _db.BusinessProfiles.Where(s => s.IsDelete.Equals(1)).SingleOrDefault(b => b.BusinessId == job.BusinessId);
                ViewData["Business"] = business;
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
            //**
            job.BusinessId = 15;
            job.PostDate = DateTime.Today;
            job.Salary = salaryFrom + "-" + salaryTo;
            job.IsDelete = 1;
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

        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            // Get the file from the POST request
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            var fileRoute = Path.Combine(webRootPath, "uploads");

            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            // Basic validation on mime types and file extension
            string[] imageMimetypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };

            try
            {
                if (Array.IndexOf(imageMimetypes, mimeType) >= 0 && (Array.IndexOf(imageExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable fileUrl = new Hashtable();
                    fileUrl.Add("link", "/uploads/" + name);

                    return Json(fileUrl);
                }
                throw new ArgumentException("The file did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Json(ex.Message);
            }
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
            //**
            job.BusinessId = 15;
            job.PostDate = DateTime.Today;
            job.Salary = salaryFrom + "-" + salaryTo;
            job.IsDelete = 1;
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

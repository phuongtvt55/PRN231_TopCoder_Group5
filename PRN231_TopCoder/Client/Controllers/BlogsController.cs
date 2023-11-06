using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using BlogService.Models;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.IO;

namespace Client.Controllers
{
    public class BlogsController : Controller
    {        
        private readonly blogServiceContext _context;
        private readonly HttpClient client;
        private string api = "";
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public BlogsController(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _context = new blogServiceContext();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44305/api/Blogs";
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(api);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Blog> list = JsonSerializer.Deserialize<List<Blog>>(data, options);
            var userId = HttpContext.Session.GetInt32("UserId");
            var businessId = HttpContext.Session.GetInt32("BusinessId");
            var role = HttpContext.Session.GetString("MyRole");
            //**
            if (userId == null)
            {
                TempData["errorMessage"] = "Please login first";
                return RedirectToAction("SetSessionData", "Users", new { id = 1 });
            }
            ViewData["UserId"] = userId;
            return View(list);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(api + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Blog blog = JsonSerializer.Deserialize<Blog>(data, options);
                return View(blog);
            }
            return NotFound();
        }


        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog, IFormFile image)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (image == null || image.Length == 0)
                return View();

            blog.Image = await UploadImage(image);
            blog.UserId = userId;
            blog.Status = "Waiting";
            blog.IsDelete = 1;
            string data = JsonSerializer.Serialize(blog);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(api, content);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(api + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Blog blog = JsonSerializer.Deserialize<Blog>(data, options);
                return View(blog);
            }
            return NotFound();
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog, IFormFile image)
        {
            if (image == null || image.Length == 0)
                return View();

            blog.Image = await UploadImage(image);
            var userId = HttpContext.Session.GetInt32("UserId");
            blog.UserId = userId;
            var blogEdit = _context.Blogs.SingleOrDefault(i => i.BlogId == id);
            blog.Status = blogEdit.Status;
            blog.IsDelete = blogEdit.IsDelete;
            string data = JsonSerializer.Serialize(blog);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(api + "/" + id, content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync(api + "/" + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };

                Blog blog = JsonSerializer.Deserialize<Blog>(data, options);
                return View(blog);
            }
            return NotFound();
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync(api + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ShowBlogList()
        {
            HttpResponseMessage response = await client.GetAsync(api);
            string data = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            List<Blog> list = JsonSerializer.Deserialize<List<Blog>>(data, options);
            return View(list);
        }
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            Blog blog = new Blog();
            HttpResponseMessage response1 = await client.GetAsync(api + "/" + id);
            if (response1.IsSuccessStatusCode)
            {
                string data1 = await response1.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                blog = JsonSerializer.Deserialize<Blog>(data1, options);
                blog.Status = status;
                string data2 = JsonSerializer.Serialize(blog);
                var content = new StringContent(data2, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = await client.PutAsync(api + "/" + id, content);
                if (response2.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowBlogList");
                }
            }
            return Ok();
        }

        public async Task<string> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return "ErrorImg";

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/blogUploads", fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "blogUploads/" + fileName;
        }
    }
}

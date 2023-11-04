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

namespace Client.Controllers
{
    public class BlogsController : Controller
    {        
        private readonly blogServiceContext _context;
        private readonly HttpClient client;
        private string api = "";

        public BlogsController()
        {
            _context = new blogServiceContext();
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            api = "https://localhost:44305/api/Blogs";
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
        public async Task<IActionResult> Create(Blog blog)
        {
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
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            string data = JsonSerializer.Serialize(blog);
            var content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(api, content);
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
    }
}

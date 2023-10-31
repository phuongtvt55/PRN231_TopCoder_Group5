using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BlogService.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace Client.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BlogsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                var response = await client.GetAsync("api/Blogs");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var blogList = JsonConvert.DeserializeObject<List<Blog>>(data);

                    // Trả kết quả cho View hoặc xử lý tiếp
                    return View(blogList);
                }
                else
                {
                    // Xử lý lỗi
                    return View("Error");
                }
            }
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/Blogs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var blog = JsonConvert.DeserializeObject<Blog>(data);

                    if (blog == null)
                    {
                        return NotFound();
                    }

                    return View(blog);
                }
                else
                {
                    return NotFound();
                }
            }
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
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(JsonConvert.SerializeObject(blog), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Blogs", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Create blog is not success!");
                    return View(blog);
                }
            }
        }


        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/Blogs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var blog = JsonConvert.DeserializeObject<Blog>(data);

                    if (blog == null)
                    {
                        return NotFound();
                    }

                    return View(blog);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44305/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize đối tượng Blog thành chuỗi JSON
                    var content = new StringContent(JsonConvert.SerializeObject(blog), Encoding.UTF8, "application/json");

                    // Gửi yêu cầu PUT để cập nhật bài viết
                    var response = await client.PutAsync($"/api/Blogs/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(blog);
                    }
                }
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

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/Blogs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var blog = JsonConvert.DeserializeObject<Blog>(data);

                    if (blog == null)
                    {
                        return NotFound();
                    }

                    return View(blog);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                // Gửi yêu cầu DELETE để xóa bài viết
                var response = await client.DeleteAsync($"/api/Blogs/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Xử lý lỗi, ví dụ hiển thị thông báo lỗi
                    return View("Error");
                }
            }
        }
    }
}

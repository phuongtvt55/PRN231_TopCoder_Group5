using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using BlogService.Models;

namespace Client.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string apiUrl = "https://localhost:44305/";

        public Comment Comment { get; private set; }

        public CommentsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44305/");
                var response = await client.GetAsync("api/Comments");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var CommentList = JsonConvert.DeserializeObject<List<Comment>>(data);

                    // Trả kết quả cho View hoặc xử lý tiếp
                    return View(CommentList);
                }
                else
                {
                    // Xử lý lỗi
                    return View("Error");
                }
            }
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/Comments/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Comment = JsonConvert.DeserializeObject<Comment>(data);

                    if (Comment == null)
                    {
                        return NotFound();
                    }

                    return View(Comment);
                }
                else
                {
                    return NotFound();
                }
            }
        }


        // GET: Comments/Create
        public async Task<IActionResult> Create()
        {
            List<Blog> blogs = new List<Blog>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("/api/Blogs");

                if (response.IsSuccessStatusCode)
                {
                    var blogData = await response.Content.ReadAsStringAsync();
                    blogs = JsonConvert.DeserializeObject<List<Blog>>(blogData);
                }
            }

            ViewBag.BlogItems = new SelectList(blogs, "BlogId", "BlogTitle");

            return View();
        }


        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Comment Comment)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(JsonConvert.SerializeObject(Comment), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/Comments", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Create Comment is not success!");
                    return View(Comment);
                }
            }
        }


        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var blogResponse = await client.GetAsync("/api/Blogs");
                if (blogResponse.IsSuccessStatusCode)
                {
                    var blogData = await blogResponse.Content.ReadAsStringAsync();
                    var blogs = JsonConvert.DeserializeObject<List<Blog>>(blogData); // Danh sách các blog

                    var commentResponse = await client.GetAsync($"/api/Comments/{id}");
                    if (commentResponse.IsSuccessStatusCode)
                    {
                        var commentData = await commentResponse.Content.ReadAsStringAsync();
                        Comment = JsonConvert.DeserializeObject<Comment>(commentData);

                        // Tạo danh sách BlogItems cho dropdown list
                        ViewBag.BlogItems = new SelectList(blogs, "BlogId", "BlogTitle", Comment.BlogId);

                        return View(Comment); // Trả về trang chỉnh sửa và truyền model Comment
                    }
                }

                return NotFound();
            }
        }





        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment Comment)
        {
            if (id != Comment.CommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Comment.CommentDate = DateTime.Now;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize đối tượng Comment thành chuỗi JSON
                    var content = new StringContent(JsonConvert.SerializeObject(Comment), Encoding.UTF8, "application/json");

                    // Gửi yêu cầu PUT để cập nhật bài viết
                    var response = await client.PutAsync($"/api/Comments/{id}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(Comment);
                    }
                }
            }

            return View(Comment);
        }


        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await client.GetAsync($"/api/Comments/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var Comment = JsonConvert.DeserializeObject<Comment>(data);

                    if (Comment == null)
                    {
                        return NotFound();
                    }

                    return View(Comment);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                var response = await client.DeleteAsync($"/api/Comments/{id}");

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

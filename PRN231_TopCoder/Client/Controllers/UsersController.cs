using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using UserService.Models;

namespace Client.Controllers
{
    public class UsersController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44359/api");
        private readonly HttpClient client = null;

        public UsersController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                var role = HttpContext.Session.GetString("MyRole");
                user.UserType = role;
                user.IsDelete = 1;
                user.Password = BCrypt.Net.BCrypt.HashPassword(Request.Form["Password"]);
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                if (role == "Employer")
                {
                    response = client.PostAsync(baseAddress + "/BusinessProfiles", content).Result;                    
                }
                else
                {
                    response = client.PostAsync(baseAddress + "/Users/register", content).Result;
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Login", "Users");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(UserLoginModel model)
        {
            var modelId = HttpContext.Session.GetInt32("ModelId");
			var role = HttpContext.Session.GetString("MyRole");            
            string data1 = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data1, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(baseAddress + "/Users/" + modelId, content).Result;
            User user = new User();

            if (response.IsSuccessStatusCode)
            {
                string data2 = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data2);
                var check = false;
                if (user.UserType == role)
                {
                    check = true;
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("Username", user.UserName);
                    if (user.BusinessProfile == null)
                    {
                        HttpContext.Session.SetInt32("BusinessId", 0);
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("BusinessId", user.BusinessProfile.BusinessId);
                    }
                    if (user.ImageProfile == null)
                    {
                        HttpContext.Session.SetString("ImageProfile", "0");
                    }
                    else
                    {
                        HttpContext.Session.SetString("ImageProfile", user.ImageProfile);
                    }
                    return RedirectToAction("Index", "Jobs");
                }
                if (user.UserType == "Admin")
                {
					HttpContext.Session.SetString("MyRole", user.UserType);
					HttpContext.Session.SetInt32("UserId", user.UserId);
					HttpContext.Session.SetString("Username", user.UserName);
					return RedirectToAction("Index", "Home");
				}
                if (!check)
                {
                    TempData["errorMessage"] = "This account is not existed";
                    return View();
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
			HttpContext.Session.Remove("MyRole");
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("BusinessId");
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("ImageProfile");
			return View("Login");
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            HttpResponseMessage response = client.GetAsync(baseAddress + "/Users").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
				users = JsonConvert.DeserializeObject<List<User>>(data);
            }
            return View(users);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                User user = new User();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/Users/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
					user = JsonConvert.DeserializeObject<User>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                User user = new User();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/Users/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(data);
                    ViewData["ImgSrc"] = user.ImageProfile;
					ViewData["CvSrc"] = user.Cvprofile;
				}
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(User user, IFormFile image, IFormFile cv)
        {
            try
            {
                if (image != null || cv != null)
                {
                    user.ImageProfile = await UploadImage(image);
                    user.Cvprofile = await UploadCv(cv);
                }
                string data = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(baseAddress + "/Users/" + user.UserId, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                User user = new User();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/Users/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    user = JsonConvert.DeserializeObject<User>(data);
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(baseAddress + "/Users/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Jobs");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View();
        }
        [HttpGet]
        public ActionResult SetSessionData(int id)
        {
			HttpContext.Session.SetInt32("ModelId", id);
			if (id == 1)
            {                
				HttpContext.Session.SetString("MyRole", "User");
            }
            else
            {
                HttpContext.Session.SetString("MyRole", "Employer");
            }
            return RedirectToAction("Login", "Users");
        }

        private async Task<string> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return "NULL";

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "img/" + fileName;
        }

        private async Task<string> UploadCv(IFormFile cv)
        {
            if (cv == null || cv.Length == 0)
                return "NULL";

            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(cv.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cv", fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await cv.CopyToAsync(stream);
            }

            return "cv/" + fileName;
        }
    }
}

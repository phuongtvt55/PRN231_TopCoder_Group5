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
        public IActionResult Register(BusinessProfile business)
        {
            try
            {
                business.User.UserType = HttpContext.Session.GetString("MyRole");
                string data = JsonConvert.SerializeObject(business);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(baseAddress + "/BusinessProfiles", content).Result;

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
        public IActionResult Login(BusinessProfile business)
        {
            var email = Request.Form["User.Email"];
            var pass = Request.Form["User.Password"];
            List<BusinessProfile> businessList = new List<BusinessProfile>();
            HttpResponseMessage response = client.GetAsync(baseAddress + "/BusinessProfiles").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                businessList = JsonConvert.DeserializeObject<List<BusinessProfile>>(data);
                foreach (var item in businessList)
                {
                    if (item.User.Email == email && item.User.Password == pass)
                    {
                        HttpContext.Session.SetInt32("UserId", item.User.UserId);
                        HttpContext.Session.SetInt32("BussinessId", item.BusinessId);
                        HttpContext.Session.SetString("Username", item.User.UserName);
                        HttpContext.Session.SetString("ImageProfile", item.User.ImageProfile);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
            }
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("MyRole");
            return View("Login");
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<BusinessProfile> businessList = new List<BusinessProfile>();
            HttpResponseMessage response = client.GetAsync(baseAddress + "/BusinessProfiles").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                businessList = JsonConvert.DeserializeObject<List<BusinessProfile>>(data);
            }
            return View(businessList);
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                BusinessProfile business = new BusinessProfile();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/BusinessProfiles/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    business = JsonConvert.DeserializeObject<BusinessProfile>(data);
                }
                return View(business);
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
                BusinessProfile business = new BusinessProfile();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/BusinessProfiles/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    business = JsonConvert.DeserializeObject<BusinessProfile>(data);
                    ViewData["ImgSrc"] = business.User.ImageProfile;
					ViewData["CvSrc"] = business.User.Cvprofile;
				}
                return View(business);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(BusinessProfile business, IFormFile image, IFormFile cv)
        {
            try
            {
                if (image != null || cv != null)
                {
                    business.User.ImageProfile = await UploadImage(image);
                    business.User.Cvprofile = await UploadCv(cv);
                }

                string data = JsonConvert.SerializeObject(business);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PutAsync(baseAddress + "/BusinessProfiles/" + business.BusinessId, content).Result;
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
                BusinessProfile business = new BusinessProfile();
                HttpResponseMessage response = client.GetAsync(baseAddress + "/BusinessProfiles/" + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    business = JsonConvert.DeserializeObject<BusinessProfile>(data);
                }
                return View(business);
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
                HttpResponseMessage response = client.DeleteAsync(baseAddress + "/BusinessProfiles/" + id).Result;
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
        public ActionResult SetSessionData(int id)
        {
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
                return "ErrorImg";

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
                return "ErrorImg";

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

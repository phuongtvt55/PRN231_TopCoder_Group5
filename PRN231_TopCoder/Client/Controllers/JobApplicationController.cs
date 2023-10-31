using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System;
using JobApplicationService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Client.Controllers
{
    public class JobApplicationController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44369/api");
        private readonly HttpClient _client;


        //private string ProductApiUrl = "";
        private readonly jobApplicationServiceContext _db;

        // constructor
        public JobApplicationController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            //tao db de truy cap vao database
            _db = new jobApplicationServiceContext();
        }

        //------------------Index------------------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<JobApplication> applicationList = new List<JobApplication>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/JobApplication/GetAllJobApplications").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                applicationList = JsonConvert.DeserializeObject<List<JobApplication>>(data);
            }

            return View(applicationList);
        }


        //---------------------Create-------------------
        //GET
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        //POST
        [HttpPost]

        public IActionResult Create(JobApplication jobA)
        {
            try
            {
                string data = JsonConvert.SerializeObject(jobA);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/JobApplication/PostJobApplication", content).Result;

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

        //---------------------Edit-------------------
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                JobApplication jobA = new JobApplication();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/JobApplication/GetJobApplication/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    jobA = JsonConvert.DeserializeObject<JobApplication>(data);
                }
                return View(jobA);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }


        [HttpPost]
        public IActionResult Edit(JobApplication jobA)
        {
            try
            {
                string data = JsonConvert.SerializeObject(jobA);
                StringContent content = new StringContent(data, Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress +
                    "/JobApplication/PutJobApplication/" + jobA.JobId, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Job Application details updated";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
            return View(jobA);
        }


        //---------------------Delete-------------------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                JobApplication jobA = new JobApplication();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/JobApplication/GetJobApplication/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    jobA = JsonConvert.DeserializeObject<JobApplication>(data);
                }
                return View(jobA);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            try
            {
                JobApplication jobA = new JobApplication();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/JobApplication/GetJobApplication/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    jobA = JsonConvert.DeserializeObject<JobApplication>(data);
                }
                return View("Delete",jobA);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int ApplicationId)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress +
                        "/JobApplication/Delete/" + ApplicationId).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product deleted";
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


        //---------------------Details-------------------
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                JobApplication jobA = new JobApplication();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/JobApplication/GetJobApplication/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    jobA = JsonConvert.DeserializeObject<JobApplication>(data);
                }
                return View(jobA);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }


    }
}

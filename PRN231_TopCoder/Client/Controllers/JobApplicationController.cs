﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using JobApplicationService.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Linq;
using JobService.Models;
using UserService.Models;

namespace Client.Controllers
{
    public class JobApplicationController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44369/api/JobApplication");
        private readonly HttpClient _client;
        private readonly jobApplicationServiceContext _context;
        private readonly jobServiceContext _jobcontext;

        // constructor
        public JobApplicationController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _context = new jobApplicationServiceContext();
            _jobcontext = new jobServiceContext();
        }

        //------------------Index------------------------
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<JobApplication> applicationList = new List<JobApplication>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress).Result;

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
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress, content).Result;

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
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id).Result;

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
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/" + jobA.JobId, content).Result;

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
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id).Result;

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
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id).Result;

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
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "/" + ApplicationId).Result;

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
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/" + id).Result;

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

        public async Task<IActionResult> ShowApplicationList()
        {
            var businessId = HttpContext.Session.GetInt32("BusinessId");
            var jobList = _jobcontext.Jobs.Where(m => m.BusinessId == businessId).ToList();
            List<JobApplication> jobApplications = new List<JobApplication>();  
            var applicationList = _context.JobApplications.ToList();
            foreach(var job in jobList)
            {
                foreach(var application in applicationList)
                {
                    if (job.JobId == application.JobId)
                    {
                        jobApplications.Add(application);
                    }                    
                }
            }
            var db = new userServiceContext();
            ViewData["User"] = db.Users.ToList();
            return View(applicationList);
        }
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            JobApplication jobApp = new JobApplication();
            HttpResponseMessage response1 = await _client.GetAsync(baseAddress + "/" + id);
            if (response1.IsSuccessStatusCode)
            {
                string data1 = await response1.Content.ReadAsStringAsync();
                jobApp = JsonConvert.DeserializeObject<JobApplication>(data1);
                jobApp.Status = status;
                string data2 = JsonConvert.SerializeObject(jobApp);
                var content = new StringContent(data2, Encoding.UTF8, "application/json");
                HttpResponseMessage response2 = await _client.PutAsync(baseAddress + "/" + id, content);
                if (response2.IsSuccessStatusCode)
                {
                    return RedirectToAction("ShowJobList");
                }
            }
            return Ok();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebCore.Controllers
{
    public class TaskDetailsController : Controller
    {
        private string BaseUrl = "http://localhost:61702/";

        // GET: TaskDetails
        // GET: TaskDetails
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/TaskDetails");

                if (response.IsSuccessStatusCode)
                {

                    var taskdetails = response.Content.ReadAsAsync<IEnumerable<TaskDetail>>().Result;

                    return View(taskdetails);
                }
            }
            return View();
        }

        // GET: TaskDetails/Details/5
        public async Task<ActionResult> Details(int id)
        {
            TaskDetail taskDetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync($"api/TaskDetails/{id}");

                if (result.IsSuccessStatusCode)
                {
                    taskDetail = await result.Content.ReadAsAsync<TaskDetail>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(taskDetail);
        }

        // GET: TaskDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaskDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskDetail taskDetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.PostAsJsonAsync<TaskDetail>("api/TaskDetails", taskDetail).Result;

                if (Result.IsSuccessStatusCode == true)
                {
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    return View();
                }
            }
        }

        // GET: TaskDetails/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            TaskDetail taskDetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/TaskDetails/{id}");

                if (response.IsSuccessStatusCode == true)
                {
                    taskDetail = await response.Content.ReadAsAsync<TaskDetail>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(taskDetail);
        }
        // POST: TaskDetails/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, TaskDetail taskDetail)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PutAsJsonAsync($"api/TaskDetails/{id}", taskDetail);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
                return RedirectToAction("Index");
            }
            return View(taskDetail);
        }

        // GET: TaskDetails/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            TaskDetail taskDetail = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var result = await client.GetAsync($"api/TaskDetails/{id}");

                if (result.IsSuccessStatusCode)
                {
                    taskDetail = await result.Content.ReadAsAsync<TaskDetail>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(taskDetail);
        }

        // POST: TaskDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, TaskDetail taskDetail)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await client.DeleteAsync($"api/TaskDetails/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }
    }
}
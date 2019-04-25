using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApi.Models;

namespace WebCore.Controllers
{
    public class DailyTasksController : Controller
    {
        private string BaseUrl = "http://localhost:61702/";
        // GET: DailyTasks
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/DailyTasks");

                if (response.IsSuccessStatusCode)
                {

                    var dailyTasks = response.Content.ReadAsAsync<IEnumerable<DailyTask>>().Result;

                    return View(dailyTasks);
                }
            }
            return View();
        }

        // GET: DailyTasks/Details/5
        public async Task<ActionResult> Details(int id)
        {
            DailyTask dailyTask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync($"api/DailyTasks/{id}");

                if (result.IsSuccessStatusCode)
                {
                    dailyTask = await result.Content.ReadAsAsync<DailyTask>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(dailyTask);
        }

        // GET: DailyTasks/Create
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseEmp = await client.GetAsync("api/Employees");
                HttpResponseMessage responseTask = await client.GetAsync("api/TaskDetails");

                if (responseEmp.IsSuccessStatusCode && responseTask.IsSuccessStatusCode)
                {

                    var employees = responseEmp.Content.ReadAsAsync<IEnumerable<Employee>>().Result.ToList();
                    var tasks = responseTask.Content.ReadAsAsync<IEnumerable<TaskDetail>>().Result.ToList();

                    ViewData["EmployeeRef"] = new SelectList(employees, "EmployeeId", "FirstName");
                    ViewData["TaskRef"] = new SelectList(tasks, "TaskId", "TaskName");

                    return View();
                }
            }
            return View();
        }

        // POST: DailyTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DailyTask dailyTask)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var Result = client.PostAsJsonAsync<DailyTask>("api/DailyTasks", dailyTask).Result;

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

        // GET: DailyTasks/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            DailyTask dailyTask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage responseDaily = await client.GetAsync($"api/DailyTasks/{id}");
                HttpResponseMessage responseEmp = await client.GetAsync("api/Employees");
                HttpResponseMessage responseTask = await client.GetAsync("api/TaskDetails");

                if (responseDaily.IsSuccessStatusCode == true && responseEmp.IsSuccessStatusCode == true && responseTask.IsSuccessStatusCode == true)
                {
                    dailyTask = await responseDaily.Content.ReadAsAsync<DailyTask>();

                    var employees = responseEmp.Content.ReadAsAsync<IEnumerable<Employee>>().Result.ToList();

                    var tasks = responseTask.Content.ReadAsAsync<IEnumerable<TaskDetail>>().Result.ToList();

                    ViewData["EmployeeRef"] = new SelectList(employees, "EmployeeId", "FirstName", dailyTask.EmployeeRef);

                    ViewData["TaskRef"] = new SelectList(tasks, "TaskId", "TaskName", dailyTask.TaskRef);


                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(dailyTask);
        }

        // POST: DailyTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, DailyTask dailyTask)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    var response = await client.PutAsJsonAsync($"api/DailyTasks/{id}", dailyTask);
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
            return View(dailyTask);
        }
        // GET: DailyTasks/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            DailyTask dailyTask = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var result = await client.GetAsync($"api/DailyTasks/{id}");

                if (result.IsSuccessStatusCode)
                {
                    dailyTask = await result.Content.ReadAsAsync<DailyTask>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(dailyTask);
        }

        // POST: TaskDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, DailyTask dailyTask)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.DeleteAsync($"api/DailyTasks/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
            }
            return View();
        }

        public async Task<ActionResult> GetTaskByEmployee(int? id, DateTime? startdate, DateTime? enddate)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get a list of tasks assigned tp a user through the DailyTaskApi repo

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                HttpResponseMessage response = await client.GetAsync("api/DailyTasks");

                if (response.IsSuccessStatusCode)
                {

                    //var dailyTasks = response.Content.ReadAsAsync<DailyTask>().ToAsyncEnumerable().Where(x => x.EmployeeRef == id);
                    var dailyTasks = response.Content.ReadAsAsync<IEnumerable<DailyTask>>().Result.Where(x => x.EmployeeRef == id);


                    if (startdate != null && enddate != null)
                    {
                        var filteredTasks = dailyTasks.Where(x => x.AssignedOn >= startdate && x.AssignedOn <= enddate);


                        //return to view with filtered data
                        return View(filteredTasks);
                    }

                    // return to view with all data
                    return View(dailyTasks);
                }

            }

            return View();

        }
    }
}
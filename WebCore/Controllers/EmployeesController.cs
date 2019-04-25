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
    public class EmployeesController : Controller
    {
        private string BaseUrl = "http://localhost:61702/";

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Employees");

                if (response.IsSuccessStatusCode)
                {

                    var employees = response.Content.ReadAsAsync<IEnumerable<Employee>>().Result;

                    return View(employees);
                }
            }

            return View();
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync($"api/Employees/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employee = await result.Content.ReadAsAsync<Employee>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(employee);
        }

        // GET: Employees/Create
        public async Task<ActionResult> Create()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Employeeroles");

                if (response.IsSuccessStatusCode)
                {

                    var employeeroles = response.Content.ReadAsAsync<IEnumerable<EmployeeRole>>().Result.ToList();

                    ViewData["EmployeeRoles"] = new SelectList(employeeroles, "RoldeId", "RoleName");

                    return View();
                }
            }
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                var Result = client.PostAsJsonAsync<Employee>("api/Employees", employee).Result;

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

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Employees/{id}");
                HttpResponseMessage responseRole = await client.GetAsync("api/EmployeeRoles");

                if (response.IsSuccessStatusCode == true && responseRole.IsSuccessStatusCode == true)
                {
                    employee = await response.Content.ReadAsAsync<Employee>();


                    var roles = responseRole.Content.ReadAsAsync<IEnumerable<EmployeeRole>>().Result.ToList();


                    ViewData["EmployeeRoleRef"] = new SelectList(roles, "RoldeId", "RoleName", employee.EmployeeRoleRef);


                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Employee employee)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PutAsJsonAsync($"api/Employees/{id}", employee);
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
            return View(employee);
        }

        // GET: TaskDetails/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Employee employee = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var result = await client.GetAsync($"api/Employees/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employee = await result.Content.ReadAsAsync<Employee>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(employee);
        }

        // POST: TaskDetails/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Employee employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                var response = await client.DeleteAsync($"api/Employees/{id}");
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
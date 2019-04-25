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
    public class EmployeeRolesController : Controller
    {
        private string BaseUrl = "http://localhost:61702/";
        // GET: EmployeeRoles
        public async Task<ActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync("api/Employeeroles");

                if (response.IsSuccessStatusCode)
                {

                    var employeeroles = response.Content.ReadAsAsync<IEnumerable<EmployeeRole>>().Result;

                    return View(employeeroles);
                }
            }
            return View();
        }

        // GET: EmployeeRoles/Details/5
        public async Task<ActionResult> Details(int id)
        {
            EmployeeRole employeeRole = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync($"api/Employeeroles/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employeeRole = await result.Content.ReadAsAsync<EmployeeRole>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(employeeRole);
        }

        // GET: EmployeeRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeRoles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var Result = client.PostAsJsonAsync<EmployeeRole>("api/EmployeeRoles", employeeRole).Result;

                    if (Result.IsSuccessStatusCode == true)
                    {
                        return RedirectToAction(nameof(Index));

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }

            return View(employeeRole);

        }

        // GET: EmployeeRoles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {

            EmployeeRole employeeRole = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"api/Employeeroles/{id}");

                if (response.IsSuccessStatusCode == true)
                {
                    employeeRole = await response.Content.ReadAsAsync<EmployeeRole>();

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(employeeRole);
        }

        // POST: EmployeeRoles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeRole employeeRole)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PutAsJsonAsync($"api/Employeeroles/{id}", employeeRole);
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
            return View(employeeRole);
        }

        // GET: EmployeeRoles/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            EmployeeRole employeeRole = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var result = await client.GetAsync($"api/Employeeroles/{id}");

                if (result.IsSuccessStatusCode)
                {
                    employeeRole = await result.Content.ReadAsAsync<EmployeeRole>();
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }

            return View(employeeRole);
        }

        // POST: EmployeeRoles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, EmployeeRole employeeRole)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);

                var response = await client.DeleteAsync($"api/Employeeroles/{id}");
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
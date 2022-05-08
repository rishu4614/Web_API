using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Web_API.Models;
using System.Web.Security;

namespace Web_API.Controllers
{
    
    public class CrudMvcController : Controller
    {
        HttpClient client = new HttpClient();
        // GET: CrudMvc

        //...........................................Display Records................................................................
        
        public ActionResult Display()
        {
            List<Employee> emplist = new List<Employee>();
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.GetAsync("crudapi");
            response.Wait();
            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<List<Employee>>();
                display.Wait();
                emplist = display.Result;

            }
            return View(emplist);
        }

        //...........................................Sign up page....................................................................
        //HTTP get request
        
        public ActionResult Signup()
        {
            return View();
        }

        //HTTP post request
        [HttpPost]

        public ActionResult Signup(Employee emp)
        {
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.PostAsJsonAsync<Employee>("crudapi", emp);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }

            return View("Create");
        }

        //...........................................Details..........................................................................
        //Http Get request
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.GetAsync("crudapi?id=" + id.ToString());
            response.Wait();
            var test = response.Result;

            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }

        //..........................................Update..........................................................................
        //Http Get request
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        //Http Post request
        [HttpPost]
        [Authorize]
        public ActionResult Edit(Employee e)
        {
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.PutAsJsonAsync<Employee>("crudapi", e);
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }

            return View("Edit");
        }
        //....................................Delete........................................
        //Http Get request
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Employee e = null;
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.GetAsync("CrudApi?id=" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                var display = test.Content.ReadAsAsync<Employee>();
                display.Wait();
                e = display.Result;
            }
            return View(e);
        }
        //Http post request
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            client.BaseAddress = new Uri("https://localhost:44372/api/crudapi");
            var response = client.DeleteAsync("crudapi/" + id.ToString());
            response.Wait();

            var test = response.Result;
            if (test.IsSuccessStatusCode)
            {
                return RedirectToAction("Display");
            }
            return View("Delete");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee emp)
        {
            using (var context = new CrudDBEntities())
            {
                bool isValid = context.Employees.Any(x => x.Username == emp.Username && x.Password == emp.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(emp.Username, false);
                    return RedirectToAction("Display");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username and password");
                    return View();
                }
            }
            
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web_API.Models;

namespace Web_API.Controllers
{
    public class CrudApiController : ApiController
    {
        CrudDBEntities db = new CrudDBEntities();

        //Display Records................................................................
        [HttpGet]
        public IHttpActionResult Display()
        {
            List<Employee> list = db.Employees.ToList();
            return Ok(list);
        }

        //Sign up page....................................................................
        [HttpPost]
        public IHttpActionResult Signup(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
            return Ok();
        }

        //Details..........................................................................

        [HttpGet]
        public IHttpActionResult Details(int id)
        {
            var emp = db.Employees.Where(model => model.Id == id).FirstOrDefault();
            return Ok(emp);
        }



        //Update..........................................................................
        [HttpPut]
        public IHttpActionResult Update(Employee e)
        {
            db.Entry(e).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            /*var emp = db.Employees.Where(model => model.Id == e.Id).FirstOrDefault();
            if(emp != null)
            {
                emp.Id = e.Id;
                emp.Username = e.Username;
                emp.Password = e.Password;
                emp.Confirm_Password = e.Confirm_Password;
                emp.Name = e.Name;
                emp.Country = e.Country;
                emp.State = e.State;
                emp.City = e.City;
                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }*/
            return Ok();
        }

        //Delete..........................................................................

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var emp = db.Employees.Where(model => model.Id == id).FirstOrDefault();
            db.Entry(emp).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }


    }
}

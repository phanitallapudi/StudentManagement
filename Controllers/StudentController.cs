using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using StudentManagement.Models;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        DbServicesContext context = new DbServicesContext();


        // GET: Student
        public ActionResult Index()
        {
            return View(context.Std_Table.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student stud)
        {
            context.Std_Table.Add(stud);
            int count = context.SaveChanges();

            if (count > 0)
            {
                ViewBag.SubmitMsg = ("<script>alert('Account created successfully, please log in.')</script>");
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.SubmitMsg = ("<script>alert('Error occured')</script>");

            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var elem = context.Std_Table.Where(model => model.Id == id).FirstOrDefault();

            return View(elem);
        }
        [HttpPost]
        public ActionResult Edit(Student stud)
        {
            context.Entry(stud).State = EntityState.Modified;
            int change = context.SaveChanges();

            if (change > 0)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.EditMdsg = ("<script>alert('Error occured')</script>");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            var changes = context.Std_Table.Where(model => model.Id == id).FirstOrDefault();
            return View(changes);
        }

        [HttpPost]
        public ActionResult Delete(Student stud)
        {
            context.Entry(stud).State = EntityState.Deleted;
            int change = context.SaveChanges();

            if (change > 0)
            {
                ModelState.Clear();
                Session.Abandon();
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.EditMdsg = ("<script>alert('Error occured')</script>");
            }
            return View();
        }


        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Student stud)
        {
            var row = context.Std_Table.Where(model => model.Email == stud.Email && model.Password == stud.Password).FirstOrDefault();
            //int count = context.SaveChanges();

            if (row != null)
            {
                Session["Name"] = row.Name;
                Session["Password"] = row.Password;
                if (row.Email == "admin" && row.Password== "admin") 
                {
                    return RedirectToAction("AdminView");
                }
                
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.LoginMsg = ("<script>alert('Incorrect credentials')</script>");
                ModelState.Clear();
            }

            return View();
        }

        public ActionResult AdminView()
        {
            return View(context.Std_Table.ToList());
        }

        public ActionResult Welcome()
        {
            //var row = context.Std_Table.Where

            return View(context.Std_Table.ToList());
        }


    }
}
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

        private bool UserExists(string name)
        {
            return context.Std_Table.Any(u => u.Name == name);
        }


        // GET: Student

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student stud)
        {
            ModelState.Clear();
            if (!UserExists(stud.Email))
            {
                context.Std_Table.Add(stud);
                int count = context.SaveChanges();

                if (count > 0)
                {
                    ViewBag.SubmitMsg = "alert('Account created successfully, please log in.');";
                    
                }
                else
                {
                    ViewBag.SubmitMsg = "alert('Error occured.');";

                }
            }
            else
            {
                ViewBag.SubmitMsg = "alert('User already exists');";
            }

            return RedirectToAction("Login");
        }
        public ActionResult Index()
        {
            //return View(context.Std_Table.ToList());
            return RedirectToAction("Login");
        }

        public ActionResult Edit(int id)
        {
            var elem = context.Std_Table.SingleOrDefault(model => model.Id == id);
            if (elem == null)
            {
                return HttpNotFound();
            }
            return View(elem);
        }

        [HttpPost]
        public ActionResult Edit(Student stud)
        {
            if (!ModelState.IsValid)
            {
                return View(stud);
            }
            context.Entry(stud).State = EntityState.Modified;
            int change = context.SaveChanges();

            if (change > 0)
            {
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.EditMdsg = ("<script>alert('Error occured')</script>");
                return View(stud);
            }
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
            var removeData = context.Std_TableInfo.Where(m => m.Id== stud.Id).FirstOrDefault();
            //context.Std_TableInfo.Remove(removeData);
            int change = context.SaveChanges();

            if (change > 0)
            {
                ModelState.Clear();
                Session.Abandon();
                return RedirectToAction("Create");
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

        public ActionResult AdminView(string searchBy, string search)
        {
            if (searchBy == "Email")
            {
                return View(context.Std_Table.Where(x => x.Email.StartsWith(search) || search == null).ToList());
            }
            else if (searchBy == "Name")
            {
                return View(context.Std_Table.Where(x => x.Name.StartsWith(search) || search == null).ToList());
            }
            else
            {
                return View(context.Std_Table.ToList());
            }
        }

        public ActionResult Welcome()
        {
            //var row = context.Std_Table.Where

            var ViewModel = new TableViewModel
            {
                Student_List_Ref = context.Std_Table.ToList(),
                Student_Info_Ref = context.Std_TableInfo.ToList()
            };

            return View(ViewModel);
        }

        public ActionResult InfoEdit(int id)
        {
            var ref_row = context.Std_Table.Where(m => m.Id == id).FirstOrDefault();
            int foreignId = ref_row.Id;

            var changes = context.Std_TableInfo.Where(model => model.Id == id).FirstOrDefault();

            if (changes == null)
            {
                changes = new StudentInfo();
                changes.Id = id;
                changes.Name = ref_row.Name;
                context.Std_TableInfo.Add(changes);
                context.SaveChanges();
            }

            return View(changes);
        }

        [HttpPost]
        public ActionResult InfoEdit(StudentInfo stud)
        {
            var existingStudentInfo = context.Std_TableInfo.FirstOrDefault(si => si.Name == stud.Name);

            if (existingStudentInfo != null)
            {
                // Update the existing StudentInfo entity with the values from the submitted form data
                existingStudentInfo.Name = stud.Name;
                existingStudentInfo.Age = stud.Age;
                existingStudentInfo.Sport = stud.Sport;
                existingStudentInfo.Achievements = stud.Achievements;

                // Save changes to the database
                context.SaveChanges();

                return RedirectToAction("Welcome");
            }
            else
            {
                ViewBag.EditMdsg = ("<script>alert('Error occured')</script>");
                return View();
            }
        }

        public ActionResult GeneralView()
        {
            return View(context.Std_TableInfo.ToList());
        }


    }
}
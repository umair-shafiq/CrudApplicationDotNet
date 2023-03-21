using CrudApplication.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CrudApplication.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student

        StudentDBEntities dbObj = new StudentDBEntities();
        public ActionResult Student(student obj)
        {
            if(obj != null)
            {
                return View(obj);
            }
            else {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult AddStudent(student model)
        {
            student obj = new student();
            if (ModelState.IsValid)
            {
                obj.ID = model.ID;
                obj.Name = model.Name;
                obj.Fname = model.Fname;
                obj.Email = model.Email;
                obj.Mobile = model.Mobile;
                obj.Description = model.Description;

                if (model.ID == 0)
                {
                    dbObj.student.Add(obj);
                    dbObj.SaveChanges();
                }
                else
                {
                    dbObj.Entry(obj).State = EntityState.Modified;
                    dbObj.SaveChanges();
                }

            }

            ModelState.Clear();
            return View("Student");
        }

        public ActionResult StudentList()
        {
            var res = dbObj.student.ToList();
            return View(res);
        }

        public ActionResult Delete(int id)
        {
            var res = dbObj.student.Where(x => x.ID == id).First();
            dbObj.student.Remove(res);
            dbObj.SaveChanges();

            var list = dbObj.student.ToList();
            return View("StudentList", list);
        }
    }
}
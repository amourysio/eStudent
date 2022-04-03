using eStudent.Data;
using eStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStudent.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db;
        public StudentController(ApplicationDbContext Db)
        {
            db = Db;
        }

        public IActionResult StudentList()
        {
            try
            {
                //var stdList = db.tbl_Student.ToList();

                var stdList = from a in db.tbl_Student
                              join b in db.tbl_Departments
                              on a.DepID equals b.ID
                              into Dep
                              from b in Dep.DefaultIfEmpty()

                              select new StudentViewModel
                              {
                                  ID = a.ID,
                                  Name = a.Name,
                                  Fname = a.Fname,
                                  Mobile = a.Mobile,
                                  Email = a.Email,
                                  Description = a.Description,
                                  DepID = a.DepID,
                                  Department = b == null ? "" : b.Department

                              };


                return View(stdList);
            }
            catch
            {
                
                return View();

            }



        }



        public IActionResult Create(StudentViewModel obj)
        {
            loadDDL();
            return View(obj);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (obj.ID == 0)
                    {
                        db.tbl_Student.Add(obj);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        db.Entry(obj).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }

                    return RedirectToAction("StudentList");
                }

                return View(obj);
            }
            catch
            {

                return RedirectToAction("StudentList");
            }
        }



        public async Task<IActionResult> DeleteStd(int id)
        {
            try
            {
                var std = await db.tbl_Student.FindAsync(id);
                if (std != null)
                {
                    db.tbl_Student.Remove(std);
                    await db.SaveChangesAsync();
                }

                return RedirectToAction("StudentList");
            }
            catch
            {

                return RedirectToAction("StudentList");
            }
        }



        private void loadDDL()
        {
            try
            {
                List<DepartamentViewModel> depList = new List<DepartamentViewModel>();
                depList = db.tbl_Departments.ToList();

                depList.Insert(0, new DepartamentViewModel { ID = 0, Department = "Please Select" });

                ViewBag.DepList = depList;

            }
            catch
            {


            }
        }
    }
}

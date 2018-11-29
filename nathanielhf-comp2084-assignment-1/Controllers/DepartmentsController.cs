using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using nathanielhf_comp2084_assignment_1.Models;

namespace nathanielhf_comp2084_assignment_1.Controllers
{
    // global varibles for multiples tests in this class

    [Authorize]
    public class DepartmentsController : Controller
    {
        // disable automatic db connection
        // private GroceryListModel db = new GroceryListModel();

        private IDepartmentsMock db;

        // default constructor, use the live db
        public DepartmentsController()
        {
            this.db = new EFDepartments();
        } 

        // mock constructor
        public DepartmentsController(IDepartmentsMock mock)
        {
            this.db = mock;
        }

        [AllowAnonymous]
        // GET: Departments
        public ActionResult Index()
        {
            return View("Index", db.Departments.ToList());
            //return View("Index");
        }

        [AllowAnonymous]
        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Department department = db.Departments.Find(id);
            Department department = db.Departments.SingleOrDefault(d => d.department_id == id);

            if (department == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.item_id = new SelectList(db.Items.OrderBy(i => i.name), "ItemId", "Name");

            return View("Create");
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "department_id,name,storage_type,aisle_number")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Save(department);
                return RedirectToAction("Index");
            }
            ViewBag.item_id = new SelectList(db.Items.OrderBy(i => i.name), "ItemId", "Name");

            return View("Create", department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Department department = db.Departments.Find(id);
            Department department = db.Departments.SingleOrDefault(d => d.department_id == id);

            if (department == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            ViewBag.item_id = new SelectList(db.Items.OrderBy(i => i.name), "ItemId", "Name");

            return View("Edit", department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "department_id,name,storage_type,aisle_number")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.Save(department);
                return RedirectToAction("Index");
            }
            //db.Entry(department).State = EntityState.Modified;
            //db.SaveChanges();
            ViewBag.item_id = new SelectList(db.Items.OrderBy(i => i.name), "ItemId", "Name");

            return View("Edit", department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            //Department department = db.Departments.Find(id);
            Department department = db.Departments.SingleOrDefault(d => d.department_id == id);

            if (department == null)
            {
                //return HttpNotFound();
                return View("Error");
            }
            return View("Delete", department);
        }

        // POST: Albums/Delete/5
        //[ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        //public ActionResult DeleteConfirmed(int id)
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Department department = db.Departments.SingleOrDefault(d => d.department_id == id);
            if (department == null)
            {
                return View("Error");
            }
            else
            {
                db.Delete(department);
                return RedirectToAction("Index");
            }
        }
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

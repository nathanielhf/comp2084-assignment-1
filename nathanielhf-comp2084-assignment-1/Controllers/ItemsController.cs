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
    [Authorize(Roles = "Administrator")]
    public class ItemsController : Controller
    {
        private GroceryListModel db = new GroceryListModel();

        [AllowAnonymous]
        // GET: Items
        public ActionResult Index()
        {
            var items = db.Items.Include(i => i.Department);
            return View(items.ToList());
        }

        [AllowAnonymous]
        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            ViewBag.department_id = new SelectList(db.Departments, "department_id", "name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "item_id,department_id,name,image,price")] Item item)
        {
            if (ModelState.IsValid)
            {
                // upload image if there is one
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        // get file path dynamically
                        string path = Server.MapPath("~/Content/Images/") + file.FileName;
                        file.SaveAs(path);

                        item.image = "/Content/Images/" + file.FileName;
                    }
                }

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.department_id = new SelectList(db.Departments, "department_id", "name", item.department_id);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            ViewBag.department_id = new SelectList(db.Departments, "department_id", "name", item.department_id);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "item_id,department_id,name,image,price")] Item item)
        {
            if (ModelState.IsValid)
            {
                // upload image if there is one
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file.FileName != null && file.ContentLength > 0)
                    {
                        // get file path dynamically
                        string path = Server.MapPath("~/Content/Images/") + file.FileName;
                        file.SaveAs(path);

                        item.image = "/Content/Images/" + file.FileName;
                    }
                }

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.department_id = new SelectList(db.Departments, "department_id", "name", item.department_id);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

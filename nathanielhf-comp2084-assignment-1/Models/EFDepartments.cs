using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nathanielhf_comp2084_assignment_1.Models
{
    public class EFDepartments : IDepartmentsMock
    {
        // connect to db
        private GroceryListModel db = new GroceryListModel();

        public IQueryable<Department> Departments { get { return db.Departments; } }

        public void Delete(Department department)
        {
            db.Departments.Remove(department);
            db.SaveChanges();
        }

        public Department Save(Department department)
        {
            if (department.department_id == 0)
            {
                // insert
                db.Departments.Add(department);
            }
            else
            {
                // update
                db.Entry(department).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return department;
        }
    }
}
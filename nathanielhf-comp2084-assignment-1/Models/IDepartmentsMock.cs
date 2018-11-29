using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nathanielhf_comp2084_assignment_1.Models
{
    public interface IDepartmentsMock
    {
        IQueryable<Department> Departments { get; }
        Department Save(Department department);
        void Delete(Department department);
    }
}

using AdvWrksDapper.Models;
using System.Collections.Generic;

namespace AdvWrksDapper.Repository
{
    public interface IDepartmentRepository
    {
        IEnumerable<Department> GetDepartments();
        Department GetDepartment(int id);
        bool DepartmentExists(int id);
        bool AddDepartment(Department deptdetails);
        bool UpdateDepartment(Department deptdetails);
        bool DeleteDepartment(int deptid);        
    }
}

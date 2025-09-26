using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL
{
    public class DepartmentService
    {
        public DepartmentService()
        {
            ApplicationDbContext context = new ApplicationDbContext();
        }
        public void UpdateDepartment(int id /*departmentViewModel*/)
        {
            // Get Department From Database By Given Id
            // call Repository.GetById(id, Context)
        }
    }
}

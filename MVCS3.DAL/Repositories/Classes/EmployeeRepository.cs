using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models.DepartmentModel;
using MVCS3.DAL.Models.EmployeeModel;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Repositories.Classes
{
    public class EmployeeRepository(ApplicationDbContext _dbContext) : GenericRepository<Employee>(_dbContext),IEmployeeRepository
    {
       
    }
}

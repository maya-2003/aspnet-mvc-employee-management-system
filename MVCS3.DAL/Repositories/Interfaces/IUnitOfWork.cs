using MVCS3.DAL.Repositories.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Repositories.Interfaces
{
    public interface IUnitOfWork
    {

        public IEmployeeRepository EmployeeRepository { get; } //Readonly
       
        public IDeprtmentRepository DepartmentRepository { get; } //Readonly
  
        int SaveChanges();
    }
}

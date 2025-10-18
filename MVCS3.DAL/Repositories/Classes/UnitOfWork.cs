using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    { 
    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IDeprtmentRepository> _departmentRepository;
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
        {
            _employeeRepository= new Lazy <IEmployeeRepository>(()=> new EmployeeRepository(dbContext));
            _departmentRepository = new Lazy<IDeprtmentRepository>(() => new DeprtmentRepository(dbContext)); ;
            _dbContext= dbContext;
        }
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDeprtmentRepository DepartmentRepository => _departmentRepository.Value;

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();    
        }
    }
}

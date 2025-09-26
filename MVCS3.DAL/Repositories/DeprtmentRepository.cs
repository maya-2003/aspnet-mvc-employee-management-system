using MVCS3.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.DAL.Repositories
{
    internal class DeprtmentRepository(ApplicationDbContext context)
    {
        private readonly ApplicationDbContext _context = context;

        public Department? GetById(int id)
        {
            var department = _context.Departments.Find(id);
            return department;

        }
        //Get All Departments
        //Add Department
        //Update Department
        //Delete Department

      
    }
}

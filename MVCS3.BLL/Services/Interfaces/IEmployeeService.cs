using MVCS3.BLL.DTOs;
using MVCS3.BLL.DTOs.DepartmentDtos;
using MVCS3.BLL.DTOs.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services.Interfaces
{
    public interface IEmployeeService
    {

        IEnumerable<EmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetById(int id);
        int AddEmployee(CreatedEmployeeDto employeeDto);
        bool DeleteEmployee(int id);
       
        int UpdateEmployee(UpdatedEmployeeDto employeeDto);
    }
}

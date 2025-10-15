using AutoMapper;
using Azure;
using MVCS3.BLL.DTOs.EmployeeDtos;
using MVCS3.BLL.Services.Interfaces;
using MVCS3.DAL.Models.EmployeeModel;
using MVCS3.DAL.Models.Shared.Enums;
using MVCS3.DAL.Repositories.Classes;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository, IMapper _mapper) : IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees()
        {

            //var employeesDto = employees.Select(emp => new EmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Email = emp.Email,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString(),
            //    IsActive = emp.IsActive,
            //    Salary = emp.Salary,
            //});

            //var employees = _employeeRepository.GetAll(e => new EmployeeDto()
            //{
            //    Id=e.Id,
            //    Name=e.Name,
            //    Salary=e.Salary,
            //    Age=e.Age,


            //}).Where(e=>e.Age > 25);
            //return employees;
            var employees = _employeeRepository.GetAll();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

            #region IEnumerable
            //var result = _employeeRepository.GetIEnumerable()
            //                                .Where(e => e.IsDeleted == false)
            //                                .Select(e => new EmployeeDto()
            //                                {
            //                                    Id=e.Id,
            //                                    Name =e.Name, 
            //                                   Age = e.Age,

            //                                }); 
            #endregion

            #region IQueryable
            //var result = _employeeRepository.GetIQueryable()
            //                                .Where(e => e.IsDeleted == false)
            //                                .Select(e => new EmployeeDto()
            //                                {
            //                                    Id = e.Id,
            //                                    Name = e.Name,
            //                                    Age = e.Age,

            //                                }); 
            #endregion
            //return result.ToList();


        }
    



        public EmployeeDetailsDto? GetById(int id)
        {
            var employee = _employeeRepository.GetById(id);
            //if (employee is null) return null;
            //var employeeDto = new EmployeeDetailsDto()
            //{
            //    Id = employee.Id,
            //    Address = employee.Address,
            //    Name = employee.Name,
            //    Gender = employee.Gender.ToString(),
            //    Email = employee.Email,
            //    EmployeeType = employee.EmployeeType.ToString(),
            //    IsActive = employee.IsActive,
            //    Age = employee.Age,
            //    PhoneNumber = employee.PhoneNumber,
            //    CreatedBy = employee.CreatedBy,
            //    CreatedOn = employee.CreatedOn,
            //    HiringDate = DateOnly.FromDateTime(employee.HiringDate),
            //    LastModifiedBy = employee.LastModifiedBy,
            //    LastModifiedOn = employee.LastModifiedon,
            //    Salary = employee.Salary,
            //};
            
            return employee is null ? null :  _mapper.Map<EmployeeDetailsDto>(employee);


        }

        public int AddEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee=_mapper.Map<Employee>(employeeDto);
            return _employeeRepository.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                return _employeeRepository.Update(employee) > 0 ? true : false;
            }
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee=_mapper.Map<Employee>(employeeDto);
            return _employeeRepository.Update(employee);
        }
    }
}
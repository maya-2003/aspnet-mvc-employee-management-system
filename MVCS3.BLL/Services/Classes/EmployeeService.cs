using AutoMapper;
using Azure;
using MVCS3.BLL.DTOs.EmployeeDtos;
using MVCS3.BLL.Services.AttachementService;
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
    public class EmployeeService(IUnitOfWork unitOfWork, IMapper _mapper,IAttachementService _attachementService) : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<EmployeeDto> GetAllEmployees(string? EmployeeSearchName)
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

            //var employees =  _unitOfWork.EmployeeRepository.GetAll(e => new EmployeeDto()
            //{
            //    Id=e.Id,
            //    Name=e.Name,
            //    Salary=e.Salary,
            //    Age=e.Age,


            //}).Where(e=>e.Age > 25);
            //return employees;
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees =  _unitOfWork.EmployeeRepository.GetAll();
            else 
                employees =  _unitOfWork.EmployeeRepository.GetAll(e => e.Name.ToLower().Contains(EmployeeSearchName.ToLower()));
            
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);
            
            #region IEnumerable
            //var result =  _unitOfWork.EmployeeRepository.GetIEnumerable()
            //                                .Where(e => e.IsDeleted == false)
            //                                .Select(e => new EmployeeDto()
            //                                {
            //                                    Id=e.Id,
            //                                    Name =e.Name, 
            //                                   Age = e.Age,

            //                                }); 
            #endregion

            #region IQueryable
            //var result =  _unitOfWork.EmployeeRepository.GetIQueryable()
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
            var employee =  _unitOfWork.EmployeeRepository.GetById(id);
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
            var employee = _mapper.Map<Employee>(employeeDto);
            
            if(employeeDto.Image is not null)
                employee.ImageName = _attachementService.Upload(employeeDto.Image, "Images");
            
            _unitOfWork.EmployeeRepository.Add(employee);
            return _unitOfWork.SaveChanges();


        }

        public bool DeleteEmployee(int id)
        {
            var employee =  _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                employee.IsDeleted = true;
                _unitOfWork.EmployeeRepository.Update(employee);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee=_mapper.Map<Employee>(employeeDto);
             _unitOfWork.EmployeeRepository.Update(employee);
            return _unitOfWork.SaveChanges();
        }
    }
}
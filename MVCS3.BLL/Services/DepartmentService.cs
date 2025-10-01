using MVCS3.BLL.DTOs;
using MVCS3.BLL.Factories;
using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models;
using MVCS3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services
{
    public class DepartmentService(IDeprtmentRepository _departmentRepository) : IDepartmentService
    {

        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var depts = _departmentRepository.GetAll();
            var departmentsToReturn = depts.Select(d => d.ToDepartmentDto());


            return departmentsToReturn;
        }

        //public Department? GetById(int id)
        //{
        //    _repository.GetById(id);
        //}
        public DepartmentDetailsDto? GetById(int id)
        {
            var dept = _departmentRepository.GetById(id);
            //if (dept is null) return null;
            //else
            //{
            //    var deptToReturn = new DepartmentDetailsDto()
            //    {
            //        Id = dept.Id,
            //        Code = dept.Code,
            //        Name = dept.Name,
            //        CreatedBy = dept.CreatedBy,
            //        LastModifiedBy = dept.LastModifiedBy,
            //        IsDeleted = dept.IsDeleted,
            //        DateOfCreation = DateOnly.FromDateTime(dept.CreatedOn),
            //        LastModifiedOn = DateOnly.FromDateTime(dept.LastModifiedOn),
            //    };
            //        return deptToReturn;

            //return dept is null ? null : new DepartmentDetailsDto()
            //{
            //    Id = dept.Id,
            //    Code = dept.Code,
            //    Name = dept.Name,
            //    CreatedBy = dept.CreatedBy,
            //    LastModifiedBy = dept.LastModifiedBy,
            //    IsDeleted = dept.IsDeleted,
            //    DateOfCreation = DateOnly.FromDateTime(dept.CreatedOn),
            //    LastModifiedOn = DateOnly.FromDateTime(dept.LastModifiedOn),

            //};

            //return dept is null ? null : new DepartmentDetailsDto(dept);
            return dept is null ? null : dept.ToDepartmentDetailsDto();

        }

        public int AddDepartment(CreatedDepartmentDto departmentDto)
        {
            var entity = departmentDto.ToEntity();
            return _departmentRepository.Add(entity);
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            return _departmentRepository.Update(departmentDto.ToEntity());
        }
        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                var res = _departmentRepository.Remove(department);
                return res > 0 ? true : false;
            }



        }


    }
}

using MVCS3.BLL.DTOs.DepartmentDtos;
using MVCS3.BLL.Factories;
using MVCS3.BLL.Services.Interfaces;
using MVCS3.DAL.Data.Contexts;
using MVCS3.DAL.Models;
using MVCS3.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        //Get All Departments
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var depts = _unitOfWork.DepartmentRepository.GetAll();
            var departmentsToReturn = depts.Select(d => d.ToDepartmentDto());


            return departmentsToReturn;
        }

        //public Department? GetById(int id)
        //{
        //    _repository.GetById(id);
        //}
        public DepartmentDetailsDto? GetById(int id)
        {
            var dept = _unitOfWork.DepartmentRepository.GetById(id);
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
           
            _unitOfWork.DepartmentRepository.Add(departmentDto.ToEntity());
           return _unitOfWork.SaveChanges();  
        }

        public int UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
return          _unitOfWork.SaveChanges() > 0 ? true : false;
            }



        }


    }
}

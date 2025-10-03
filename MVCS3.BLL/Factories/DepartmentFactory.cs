using MVCS3.BLL.DTOs;
using MVCS3.DAL.Models;
using MVCS3.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Factories
{
    static class DepartmentFactory
    {
        public static DepartmentDto ToDepartmentDto (this Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
            };
        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                IsDeleted = department.IsDeleted,
                DateOfCreation = DateOnly.FromDateTime(department.CreatedOn),
                LastModifiedOn = DateOnly.FromDateTime(department.LastModifiedon),

            };
            
        }

        public static Department ToEntity(this CreatedDepartmentDto dto) {
            return new Department()
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };

}
        public static Department ToEntity(this UpdateDepartmentDto dto)
        {
            return new Department()
            {
                Id= dto.Id,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation.ToDateTime(new TimeOnly())
            };

        }
    }
}

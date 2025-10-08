using AutoMapper;
using MVCS3.BLL.DTOs.EmployeeDtos;
using MVCS3.DAL.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.MappingProfiles
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles() {
            //CreateMap<Employee, EmployeeDto>();//From Employee To Employee Dto
            //CreateMap <EmployeeDto, Employee>();//From Employee Dto To Employee
            CreateMap <Employee, EmployeeDto> ().ReverseMap();//From Employee To Employee Dto
            CreateMap<Employee, EmployeeDetailsDto>().ReverseMap();

            CreateMap<CreatedEmployeeDto, EmployeeDto>().ReverseMap();
            CreateMap<UpdatedEmployeeDto, EmployeeDto>().ReverseMap();
        }
    }
}

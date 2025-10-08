using AutoMapper;
using Microsoft.Extensions.Options;
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
            CreateMap<Employee, EmployeeDto>()
                        .ForMember(dest => dest.Gender, Options => Options.MapFrom(src => src.Gender))
                        .ForMember(dest => dest.EmployeeType, Options => Options.MapFrom(src => src.EmployeeType))
                        .ReverseMap();//From Employee To Employee Dto
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HiringDate)))
                .ReverseMap();


            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                .ReverseMap();
            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(new TimeOnly())))
                .ReverseMap();
        }
    }
}

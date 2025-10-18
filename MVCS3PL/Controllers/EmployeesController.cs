using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MVCS3.BLL.DTOs.DepartmentDtos;
using MVCS3.BLL.DTOs.EmployeeDtos;
using MVCS3.BLL.Services.Classes;
using MVCS3.BLL.Services.Interfaces;
using MVCS3.DAL.Models.EmployeeModel;
using MVCS3.DAL.Models.Shared.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using MVCS3PL.ViewModels.EmployeeViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MVCS3PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, [FromServices] IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index(string? EmployeeSearchName) //Model Binding
        {
            var employees = _employeeService.GetAllEmployees(EmployeeSearchName); //Local Filter
            return View(employees);
        }

        #region Create

        [HttpGet]
        public IActionResult Create(/*[FromServices]IDepartmentService _departmentService*/)
        {
            //ViewData["Departments"] = _departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dto = new CreatedEmployeeDto()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Age = model.Age,
                        Address = model.Address,
                        DepartmentId = model.DepartmentId,
                        EmployeeType = model.EmployeeType,
                        Gender = model.Gender,
                        HiringDate = model.HiringDate,
                        IsActive = model.IsActive,
                        PhoneNumber = model.PhoneNumber,
                        Salary = model.Salary,
                        Image=model.Image

                    };
                    int res = _employeeService.AddEmployee(dto);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Added");
                        return View(model);
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1) Development => Log Error In Console And Return The Same View With The Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(model);
                    }
                    else
                    {
                        //2) Development => Log Error In File Table And Return The Same View With The Error Message
                        //_logger.LogError(ex.Message);
                        return View(model);
                    }
                }

            }
            else
            {
                return View(model);
            }

        }

        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); //400
            var emp = _employeeService.GetById(id.Value);
            if (emp is null) return NotFound();//404
            return View(emp);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest(); //480
            var emp = _employeeService.GetById(id.Value);
            if (emp is null) return NotFound();//484
            var dto = new EmployeeViewModel()
            {
                //Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
                Gender = Enum.Parse<Gender>(emp.Gender),
                DepartmentId = emp.DepartmentId

            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, EmployeeViewModel model)
        {
            //if (id != dto.Id) return BadRequest();

            var dto = new UpdatedEmployeeDto()
            {
                Id = id,
                Name = model.Name,
                Email = model.Email,
                Age = model.Age,
                Address = model.Address,
                DepartmentId = model.DepartmentId,
                EmployeeType = model.EmployeeType,
                Gender = model.Gender,
                HiringDate = model.HiringDate,
                IsActive = model.IsActive,
                PhoneNumber = model.PhoneNumber,
                Salary = model.Salary

            };
            if (!ModelState.IsValid) return View(model);
            try
            {
                var res = _employeeService.UpdateEmployee(dto);
                if (res > 0) return RedirectToAction(nameof(Index));
                return View(dto);

            }
            catch (Exception ex)
            {
                //Log Exception
                if (_environment.IsDevelopment())
                {
                    //1) Development => Log Error In Console And Return The Same View With The Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(model);
                }
                else
                {
                    //2) Development => Log Error In File Table And Return The Same View With The Error Message
                    //_logger.LogError(ex.Message);
                    return View(model);
                }

            }

        }
        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            try
            {

                bool isDeleted = _employeeService.DeleteEmployee(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else ModelState.AddModelError(string.Empty, "Employee Can't Be Deleted");
                return RedirectToAction(nameof(Delete), new { id });

            }
            catch (Exception ex)
            {
                //Log Exception
                if (_environment.IsDevelopment())
                {
                    //1) Development => Log Error In Console And Return The Same View With The Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    //2) Development => Log Error In File Table And Return The Same View With The Error Message
                    //_logger.LogError(ex.Message);
                    return View("ErrorView", ex);
                }
            }
            
        }
        #endregion
    }
}

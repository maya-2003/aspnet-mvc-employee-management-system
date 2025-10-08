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

namespace MVCS3PL.Controllers
{
    public class EmployeesController(IEmployeeService _employeeService, [FromServices] IWebHostEnvironment _environment) : Controller
    {
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedEmployeeDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _employeeService.AddEmployee(dto);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Employee Can't Be Added");
                        return View(dto);
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1) Development => Log Error In Console And Return The Same View With The Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(dto);
                    }
                    else
                    {
                        //2) Development => Log Error In File Table And Return The Same View With The Error Message
                        //_logger.LogError(ex.Message);
                        return View(dto);
                    }
                }

            }
            else
            {
                return View(dto);
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
            var dto = new UpdatedEmployeeDto()
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age,
                Address = emp.Address,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Salary = emp.Salary,
                HiringDate = emp.HiringDate,
                IsActive = emp.IsActive,
                EmployeeType = Enum.Parse<EmployeeType>(emp.EmployeeType),
                Gender = Enum.Parse<Gender>(emp.Gender)

            };
            return View(dto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(dto);
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
                    return View(dto);
                }
                else
                {
                    //2) Development => Log Error In File Table And Return The Same View With The Error Message
                    //_logger.LogError(ex.Message);
                    return View(dto);
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

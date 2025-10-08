using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MVCS3.BLL.DTOs;
using MVCS3.BLL.DTOs.DepartmentDtos;
using MVCS3.BLL.Services.Interfaces;
using MVCS3.DAL.Models;
using MVCS3PL.ViewModels.DepartmentViewModels;
namespace MVCS3PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService, ILogger<HomeController> _logger, IWebHostEnvironment _environment) : Controller
    {
        //Get BaseUrl/Department/Index
        [HttpGet]
        public IActionResult Index()
        {
            var deparatments = _departmentService.GetAllDepartments();
            return View(deparatments);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res = _departmentService.AddDepartment(departmentDto);
                    if (res > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Can't Be Added");
                        return View(departmentDto);
                    }

                }
                catch (Exception ex)
                {
                    //Log Exception
                    if (_environment.IsDevelopment())
                    {
                        //1) Development => Log Error In Console And Return The Same View With The Error Message
                        ModelState.AddModelError(string.Empty, ex.Message);
                        return View(departmentDto);
                    }
                    else
                    {
                        //2) Development => Log Error In File Table And Return The Same View With The Error Message
                        //_logger.LogError(ex.Message);
                        return View(departmentDto);
                    }


                }

            }
            else
            {
                return View(departmentDto);
            }

        }
        #endregion

        #region Details
        [HttpGet]

        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest(); //400
            var department = _departmentService.GetById(id.Value);
            if (department is null) return NotFound();//404
            return View(department);
        }
        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetById(id.Value);
            if (department is null) return NotFound();
            var deptViewModel = new DepartmentEditViewModel()
            {
                //Id = id.Value,
                Name = department.Name,
                Code = department.Code,
                DateOfCreation = department.DateOfCreation,
                Description = department.Description
            };
            return View(deptViewModel);


        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            try
            {
                var updateDeptDto = new UpdateDepartmentDto()
                {
                    Id = id,
                    Code = viewModel.Code,
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    DateOfCreation = viewModel.DateOfCreation
                };

                var res = _departmentService.UpdateDepartment(updateDeptDto);
                if (res > 0) return RedirectToAction(nameof(Index));
                return View(viewModel);

            }
            catch (Exception ex)
            {
                //Log Exception
                if (_environment.IsDevelopment())
                {
                    //1) Development => Log Error In Console And Return The Same View With The Error Message
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(viewModel);
                }
                else
                {
                    //2) Development => Log Error In File Table And Return The Same View With The Error Message
                    //_logger.LogError(ex.Message);
                    return View(viewModel);
                }


            }
        }
        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            if (!id.HasValue) return BadRequest();
            var dept= _departmentService.GetById(id.Value);
            if (dept is null) return NotFound();
            return View(dept);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if(id == 0) return BadRequest(); 
            try {

                bool isDeleted = _departmentService.DeleteDepartment(id);
                if (isDeleted)
                    return RedirectToAction(nameof(Index));
                else ModelState.AddModelError(string.Empty ,"Department Can't Be Dolotod");
                return RedirectToAction(nameof(Delete), new {id});

            }
            catch(Exception ex)
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

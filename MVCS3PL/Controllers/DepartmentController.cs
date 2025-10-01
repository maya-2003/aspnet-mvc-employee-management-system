using Microsoft.AspNetCore.Mvc;
using MVCS3.BLL.Services;
namespace MVCS3PL.Controllers
{
    public class DepartmentController(IDepartmentService _departmentService) : Controller
    {
        //Get BaseUrl/Department/Index
        [HttpGet]
        [HttpGet]
        public IActionResult Index()
        {
            var deparatments = _departmentService.GetAllDepartments();
            return View(deparatments);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.DTOs
{
    public class CreatedDepartmentDto
    {
        [MaxLength(10)]
        public string Name {get; set; }
        public string Code { get; set; }
        [Display (Name = "Creation Date")]
        public DateOnly DateOfCreation { get; set; }
        public string? Description { get; set; }
    }

}
    

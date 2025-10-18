using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services.AttachementService
{
    public interface IAttachementService
    {
        public string? Upload(IFormFile file, string folderNmae);
        public bool Delete(string filePath);
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCS3.BLL.Services.AttachementService
{
    public class AttachementService : IAttachementService
    {
        List<string> AllowedExtentions = [".png", ".jpg", ".jpeg"];
        const int _maxSize = 2097152;

        public string? Upload(IFormFile file, string folderNmae)
        {
            //Check extension
            var extention = Path.GetExtension(file.FileName);
            if (!AllowedExtentions.Contains(extention)) return null;

            //Check size
            if (file.Length == 0  || file.Length > _maxSize) return null;

            //Get Located Folder Path
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\Images\\";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "Images");
            //4. Make Attachment Name Unique GUID
     
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);
            //6. Create File Stream To Copy File [Unmanaged]
            using FileStream fs = new FileStream(filePath, FileMode.Create);
            //7. Use Stream To Copy File
            file.CopyTo(fs);
            //8.Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath))
                return false;
            File.Delete(filePath);
            return true;
        }
    }
}

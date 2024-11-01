//namespace OtoServisSatis.WebUI.Utils
//{
//    public class FileHelper
//    {
//        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/Img/")
//        {
//            var fileName = "";

//            if (formFile is not null && formFile.Length > 0) 
//            { 
//                fileName = formFile.FileName;
//                string directory = Directory.GetCurrentDirectory() + "/wwwroot/" + filePath + fileName;
//                using var stream = new FileStream(directory, FileMode.Create);
//                await formFile.CopyToAsync(stream);
//            }

//            return fileName;

//        }
//    }
//}

using System.IO;

namespace OtoServisSatis.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/Img/")
        {
            var fileName = "";

            if (formFile is not null && formFile.Length > 0)
            {
                // Get the original file name without extension
                var originalFileName = Path.GetFileNameWithoutExtension(formFile.FileName);
                // Get the file extension
                var fileExtension = Path.GetExtension(formFile.FileName);
                // Create a unique file name by appending current timestamp
                var timestamp = DateTime.Now.ToString("HHmmss");
                fileName = $"{originalFileName}{timestamp}{fileExtension}";

                string directory = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/" + filePath + fileName);
                using var stream = new FileStream(directory, FileMode.Create);
                await formFile.CopyToAsync(stream);
            }

            return fileName;
        }
    }
}

using System.Threading.Tasks;
using CSharpLab_Cursed_Ache.Model;
using CSharpLab_Cursed_Ache.Service;
using Microsoft.AspNetCore.Mvc;

namespace CSharpLab_Cursed_Ache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadFileController : ControllerBase
    {
       
        private FileManager fileManager;
        public DownloadFileController(FileManager fileManager)
        {
           this.fileManager = fileManager;
        }              
        

        [HttpPost]
        public async Task<IActionResult> Download(DownloadRequest req)
        {
            byte[] byteArray;
            string fileName = "result", mimeType;
            FileManager.FileExstensions ext;
            if (req.Type == "docx")
            {
                mimeType = "application/msword";
                ext = FileManager.FileExstensions.DOCX;

            }
            else
            {
                mimeType = "text/plain";
                ext = FileManager.FileExstensions.TXT;
            }
            byteArray = fileManager.GetFile(fileName, req.Text, ext);
            return File(byteArray, mimeType);
        }
    }
}
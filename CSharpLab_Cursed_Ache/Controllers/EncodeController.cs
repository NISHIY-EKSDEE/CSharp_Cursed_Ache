using System;
using CSharpLab_Cursed_Ache.Model;
using CSharpLab_Cursed_Ache.Service;
using GemBox.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSharpLab_Cursed_Ache.Controllers
{
    
    [ApiController]
    public class EncodeController : ControllerBase
    {

        private readonly VigenereEncoder encoder;
        private readonly FileManager fileManager;
        

        public EncodeController(VigenereEncoder encoder, FileManager fileManager)
        {
            this.encoder = encoder;
            this.fileManager = fileManager;
        }

        [HttpPost]
        [Consumes("application/json")]
        [Route("api/encodetext")]
        public VigenereEncodeResponse EncodeText(VigenereEncodeRequest request)
        {
            var text = request.Text;
            var key = request.Key;
            var action = request.Action;
            var response = new VigenereEncodeResponse();
            try
            {
                switch (action)
                {
                    case "encrypt":
                        response.Result = encoder.Encrypt(text, key);
                        response.IsSuccess = true;
                        break;
                    case "decrypt":
                        response.Result = encoder.Decrypt(text, key);
                        response.IsSuccess = true;
                        break;
                    default:
                        response.IsSuccess = false;
                        response.ErrorMessage = "UNKNOWN ACTION";
                        break;
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.ErrorMessage = $"BAD REQUEST. SHAME ON YOU. {e.Message}";
            }
            return response;
        }

        [HttpPost]
        [Route("api/encodefile")]
        public VigenereEncodeResponse EncodeFile(IFormCollection data, IFormFile file)
        {
            string action = data["action"];
            string text, key;
            try
            {
                fileManager.ParseFile(file, out text, out key);
            }
            catch (FreeLimitReachedException)
            {
                var response = new VigenereEncodeResponse() { IsSuccess = false, ErrorMessage = $"Я не заплатил 700$ за либу, которую использую для работы с файлами Word, поэтому не загружайте такой длинный текст)))" };
                return response;
            }
            catch (Exception e)
            {
                var response = new VigenereEncodeResponse() { IsSuccess = false, ErrorMessage = $"BAD REQUEST. SHAME ON YOU. {e.Message}" };
                return response;
            }
            var req = new VigenereEncodeRequest() { Text = text, Key = key, Action = action };
            return EncodeText(req);
        }




    }
}
using CSharpLab_Cursed_Ache.Exceptions;
using GemBox.Document;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpLab_Cursed_Ache.Service
{
    public class FileManager
    {
        private const string DOWNLOAD_PATH = @"Documents\";
        public FileManager()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public byte[] GetFile(string name, string text, FileManager.FileExstensions ext)
        {
            Func<string, string, byte[]> func;
            switch (ext)
            {
                case FileExstensions.DOCX:
                    func = GetMSWordFile;
                    break;
                case FileExstensions.TXT:
                    func = GetTxtFile;
                    break;
                default:
                    return null;
            }
            return func(name, text);
        }

        public void ParseFile(IFormFile file, out string text, out string key)
        {
            LoadOptions lo;
            switch (file.FileName.Split('.').Last())
            {
                case "txt":
                    lo = LoadOptions.TxtDefault;
                    break;
                case "docx":
                    lo = LoadOptions.DocxDefault;
                    break;
                case "doc":
                    lo = LoadOptions.DocDefault;
                    break;
                default:
                    throw new IncorrectFormatException("Некорректный формат файла.");
            }
            
                DocumentModel doc = DocumentModel.Load(file.OpenReadStream(), lo);
                var str = doc.Content.ToString().ToLower();
                if (str.Contains("text:") && str.Contains("key:"))
                {
                    int indexOfText = str.IndexOf("text:");
                    int indexOfKey = str.IndexOf("key:");
                    text = str.Substring(indexOfText, indexOfKey - indexOfText);
                    text = text.Replace("text:", "");
                    key = str.Substring(indexOfKey);
                    key = key.Replace("key:", "");
                }
                else
                {
                    throw new IncorrectContentException("Некорректное содержимое файла.");
                }
            
            
        }

        private byte[] GetTxtFile(string name, string text)
        {
            
            File.WriteAllText($"{DOWNLOAD_PATH}{name}.txt", text);
            return File.ReadAllBytes($"{DOWNLOAD_PATH}{name}.txt");
        }

        private byte[] GetMSWordFile(string name, string text)
        {
            DocumentModel document = new DocumentModel();
            document.Content.LoadText(text);
            document.Save($"{DOWNLOAD_PATH}{name}.docx");
            return File.ReadAllBytes($"{DOWNLOAD_PATH}{name}.docx");
        }
        
        public enum FileExstensions
        {
            DOCX,
            TXT
        }
    }
}

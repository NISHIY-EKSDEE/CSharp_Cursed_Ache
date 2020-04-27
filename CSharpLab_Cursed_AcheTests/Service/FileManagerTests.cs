using CSharpLab_Cursed_Ache.Service;
using GemBox.Document;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpLab_Cursed_Ache.Service.Tests
{
    [TestClass()]
    public class FileManagerTests
    {
        [TestMethod()]
        public void GetFileTest()
        {
            //arrange
            string text = $"random текст 123241 ~";
            string name = "test";
            var fileManager = new FileManager();

            //act
            string res1, res2;
            var byteArr1 = fileManager.GetFile(name, text, FileManager.FileExstensions.DOCX);

            using (var ms1 = new MemoryStream(byteArr1))
            {
                DocumentModel doc = DocumentModel.Load(ms1, LoadOptions.DocxDefault);
                res1 = doc.Content.ToString();
            }

            var byteArr2 = fileManager.GetFile(name, text, FileManager.FileExstensions.TXT);
            using (var ms2 = new MemoryStream(byteArr2))
            {
                DocumentModel doc = DocumentModel.Load(ms2, LoadOptions.TxtDefault);
                res2 = doc.Content.ToString();
            }

            //assert
            Assert.AreEqual(text, res1.Trim());
            Assert.AreEqual(text, res2.Trim());
        }
    }
}
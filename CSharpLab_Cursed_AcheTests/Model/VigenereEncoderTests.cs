using CSharpLab_Cursed_Ache.Exceptions;
using CSharpLab_Cursed_Ache.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpLab_Cursed_Ache.Model.Tests
{
    [TestClass]
    public class VigenereEncoderTests
    {
        [TestMethod]
        public void EncryptTest()
        {
            // arrange
            string text = "АЁЙ цйуфы 123 !э ю.,:'~sadawq";
            string key1 = "пельмень";
            string key2 = "увалень";
            string key3 = "кекс";
            string expected1 = "пкх тцшвч 123 !м г.,:'~sadawq";
            string expected2 = "узй вобро 123 !я ю.,:'~sadawq";
            string expected3 = "ккф зфшям 123 !з г.,:'~sadawq";
            var ve = new VigenereEncoder();

            // act
            string ans1 = ve.Encrypt(text, key1);
            string ans2 = ve.Encrypt(text, key2);
            string ans3 = ve.Encrypt(text, key3);

            // assert
            Assert.AreEqual(expected1, ans1);
            Assert.AreEqual(expected2, ans2);
            Assert.AreEqual(expected3, ans3);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectKeyException))]
        public void EncryptWithKeyExceptionTest()
        {
            // arrange
            string text = "АЁЙ цйуфы 123 !э ю.,:'~sadawq";
            string key = "пелENGLISH_LEtterOMG213131231~нь";
            
            var ve = new VigenereEncoder();

            // act
            ve.Encrypt(text, key);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectTextException))]
        public void EncryptWithTextExceptionTest()
        {
            // arrange
            string text = "       ";
            string key = "пелнь";

            var ve = new VigenereEncoder();

            // act
            ve.Encrypt(text, key);
        }

        [TestMethod]
        public void DecryptTest()
        {
            // arrange
            string text1 = "пкх тцшвч 123 !м г.,:'~sadawq";
            string text2 = "узй вобро 123 !я ю.,:'~sadawq";
            string text3 = "ккф зфшям 123 !з г.,:'~sadawq";
            string key1 = "пельмень";
            string key2 = "увалень";
            string key3 = "кекс";
            string expected = "аёй цйуфы 123 !э ю.,:'~sadawq";
            var ve = new VigenereEncoder();

            // act
            string ans1 = ve.Decrypt(text1, key1);
            string ans2 = ve.Decrypt(text2, key2);
            string ans3 = ve.Decrypt(text3, key3);

            // assert
            Assert.AreEqual(expected, ans1);
            Assert.AreEqual(expected, ans2);
            Assert.AreEqual(expected, ans3);
        }
    }
}
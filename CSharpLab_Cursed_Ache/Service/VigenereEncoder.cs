using CSharpLab_Cursed_Ache.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CSharpLab_Cursed_Ache.Service
{
    public class VigenereEncoder
    {
        private const string ALPHABET = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public string Encrypt(string text, string key)
        {
            key = new string(key.Where(e => !Char.IsControl(e)).ToArray());
            if (!key.All(c => ALPHABET.Contains(c)))
                throw new IncorrectKeyException("Неверный ключ. Поддерживаются только буквы русского алфавита.");
            if (string.IsNullOrWhiteSpace(text))
                throw new IncorrectTextException("Неверный текст.");
            StringBuilder answer = new StringBuilder();
            // Число, отражающее номер рассматриваемого символа РУССКОГО алфавита 
            // (как если бы строка состояла только из таких символов)
            int j = 0;
            for(int i = 0; i < text.Length; i++)
            {
                char c = Char.ToLower(text[i]);
                if (ALPHABET.Contains(c)) 
                {
                    //Строка по символу ключа (сдвиг алфавита вправо, начиная с 0)
                    int row = ALPHABET.IndexOf(key[j % key.Length]);

                    //Столбец по символу текста
                    int col = ALPHABET.IndexOf(c);

                    //В ответ добавляем букву исходного текста со сдвигом вправо по номеру строки
                    answer.Append(ALPHABET[(col + row) % ALPHABET.Length]);
                    j++;
                }
                else
                {
                    answer.Append(text[i]);
                }
            }
            return answer.ToString();
        }

        public string Decrypt(string text, string key)
        {
            key = new string(key.Where(e => !Char.IsControl(e)).ToArray());
            if (!key.All(c => ALPHABET.Contains(c)))
                throw new IncorrectKeyException("Неверный ключ. Поддерживаются только буквы русского алфавита.");
            if (string.IsNullOrWhiteSpace(text))
                throw new IncorrectTextException("Неверный текст.");
            StringBuilder answer = new StringBuilder();
            // Число, отражающее номер рассматриваемого символа РУССКОГО алфавита 
            // (как если бы строка состояла только из таких символов)
            int j = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = Char.ToLower(text[i]);
                if (ALPHABET.Contains(c))
                {
                    //Строка по символу ключа (сдвиг алфавита влево, начиная с 0)
                    int row = ALPHABET.IndexOf(key[j % key.Length]);

                    //Столбец исходного символа (сдвиг_вправо = размер_алфавита - сдвиг_влево)
                    int col = (ALPHABET.IndexOf(c) + ALPHABET.Length - row) % ALPHABET.Length;

                    //В ответ добавляем букву по номеру столбца
                    answer.Append(ALPHABET[col]);
                    j++;
                }
                else
                {
                    answer.Append(text[i]);
                }
            }
            return answer.ToString();
        }
    }
}
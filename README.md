### Для работы программы требуется установленный Node.js [link](https://nodejs.org)
Веб приложение, позволяющее зашифровать или расшифровать текст (преобразуются только русские буквы),
используя шифр Виженера. Для алгоритма требуется ключ, состоящий только из букв русского алфавита.

Основной функционал:
- Возможность зашифровать или расшифровать текст, используя ключ
- Отправка данных, введенных в специальные формы с клавиатуры
- Отправка данных, указанных в .txt или .docx файле
- Вывод результата на экран
- Возможность скачать результат в .txt или .docx формате
--------------------------------------------------------------------------

***Front-end:***
React + Redux,<br/>
Компоненты из Reactstrap

***Back-end:***
ASP .NET Core Web API

Для работы с файлами Word используется пакет GemBox.Document<br/>

Остальные пакеты:<br/>

Microsoft.AspNetCore.SpaServices.Extensions (3.1.3)<br/>
Microsoft.TypeScript.Compiler (3.1.5)<br/>
Microsoft.VisualStudio.Web.CodeGeneration.Design (3.1.2)<br/>
React.AspNet (5.2.4)

# WebShell

![Screenshot](scr.jpg)

На сервере используется CMD

## ASP.NET Core 6 MVC

Использованные материалы:\
Metanit\
Документация ASP.NET\
https://www.youtube.com/watch?v=hZ1DASYd9rk&t=8691s&ab_channel=freeCodeCamp.org\

Применен паттерн MVC:\
Создана модель Command (CommandModel)\
Создан контроллер для отображения страницы\
Создан контроллер для взаимодействия с БД (ApiController):\
API/GetCommands (GET) -- возвращает историю запросов\
API/ExecuteCommand (POST) -- выполняет запрос и возвращает результат\

Запуск осуществляется через IIS Express

# Репозиторий для работы с заданиями курса Otus "C# Developer. Professional"


## Задание 1:
1. Создать базу данных PostgreSQL для одной из компаний на выбор: Авито, СберБанк, Otus или eBay. 
Написать скрипт создания 3 таблиц, которые должны иметь первичные ключи и быть соединены внешними ключами.
2. Написать скрипт заполнения таблиц данными, минимум по пять строк в каждую.
3. Создать консольную программу, которая выводит содержимое всех таблиц.
4. Добавить в программу возможность добавления в таблицу на выбор.

### Что было сделано?

Набор проектов находится в папке решения "Task-1", содержит в себе три проекта.
- DbConsole (исполняющий консоль-проект, содержит логику получения данных от пользователя с помощьюю консоли)
- DbConsole.Application (основная логика, модели приложения, конвертеры, сервисы инициализации и работы с БД)
- DbConsole.Infrastructure (контекст и сущности БД, скрпит для второго задания)

В проекте DbConsole содержится файл appsettings.json, в который необходимо добавить строку подключения к БД.

---

## Задание 2:
1. Создать эндпоинты в проекте WebApi (https://gitlab.com/otus-education/dotnetdev.homework.7)
2. Доработать консольное приложение, чтобы оно удовлетворяло следующим требованиям:
3. Принимает с консоли ID "Клиента", запрашивает его с сервера и отображает его данные по пользователю;
4. Генерирует случайным образом данные для создания нового "Клиента" на сервере;
5. Отправляет данные, созданные в пункте 2.2., на сервер;
6. По полученному ID от сервера запросить созданного пользователя с сервера и вывести на экран.

---

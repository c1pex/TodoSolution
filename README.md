# Todo.Core

[![CI / Build & Test](https://github.com/c1pex/TodoSolution/actions/workflows/ci.yml/badge.svg)](https://github.com/c1pex/TodoSolution/actions/workflows/ci.yml)

Simple Todo core library (for practice). Библиотека для управления списком задач (ToDo) на платформе .NET 8 с поддержкой персистентности в JSON.

## Подключение пакета через NuGet (GitHub Packages)

Пакет опубликован в реестре GitHub Packages учетной записи `c1pex`.

### 1. Добавление источника пакетов

#### Вариант A: Через .NET CLI
Выполните команду в терминале, указав ваш Personal Access Token (PAT) с областью видимости `read:packages`:
```bash
dotnet nuget add source "https://nuget.pkg.github.com/c1pex/index.json" --name github --username c1pex --password <PAT> --store-password-in-clear-text
```

#### Вариант B: Через Visual Studio
1. Откройте **Tools** → **Options** → **NuGet Package Manager** → **Package Sources**.
2. Нажмите зелёный плюс (**Add**).
3. Укажите:
   - **Name**: `GitHub Packages`
   - **Source**: `https://nuget.pkg.github.com/c1pex/index.json`
4. При запросе авторизации введите логин `c1pex` и токен PAT с правом `read:packages`.

### 2. Установка пакета в проект

```bash
dotnet add package Todo.Core --version 0.2.0
```

*(или версия `0.1.0`)*

## Пример использования

```csharp
using System;
using Todo.Core;

// Создание списка задач
var todoList = new TodoList();

// Добавление новых задач
var item1 = todoList.Add("Купить молоко");
var item2 = todoList.Add("Прочитать книгу по C#");

// Отметка задачи как выполненной
item1.MarkDone();

// Поиск задач по подстроке (без учета регистра)
var searchResults = todoList.Find("молоко");
foreach (var item in searchResults)
{
    Console.WriteLine($"[{(item.IsDone ? "X" : " ")}] {item.Title} (ID: {item.Id})");
}

// Сохранение списка в JSON-файл
todoList.Save("todos.json");

// Загрузка списка из JSON-файла
var restoredList = new TodoList();
restoredList.Load("todos.json");

// Удаление задачи по идентификатору
restoredList.Remove(item2.Id);
```

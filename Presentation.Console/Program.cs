using System;
using Model;

namespace ConsoleApp
{
    class Program
    {
        // Создаём один объект Logic на всё приложение — через него работаем со всеми машинами
        static Logic logic = new Logic();

        /// <summary>
        /// Точка входа в программу. Показывает меню и обрабатывает выбор пользователя в цикле.
        /// </summary>
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Menu();
                Console.Write($"Введите ваш выбор: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        DeleteCar();
                        break;
                    case "3":
                        AllCars();
                        break;
                    case "4":
                        UpdateCar();
                        break;
                    case "5":
                        GroupBrand();
                        break;
                    case "6":
                        CarsYear();
                        break;
                    case "7":
                        CarsColor();
                        break;
                    case "8":
                        SecretFunctions();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.WriteLine("Нажмите любую клавишу для продолжения ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя целое число и повторяет запрос, пока ввод некорректный.
        /// </summary>
        static int ReadInt(string prompt)
        {
            int result;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.WriteLine("Некорректное число, попробуйте снова.");
                Console.Write(prompt);
            }
            return result;
        }

        /// <summary>
        /// Проверяет, есть ли вообще машины в базе. Если список пуст — выводит сообщение,
        /// делает паузу и возвращает true, чтобы вызывающий метод мог сразу выйти.
        /// </summary>
        static bool NoCars()
        {
            if (logic.AllCars().Count == 0)
            {
                Console.WriteLine("Список машин пуст.");
                Console.WriteLine("Нажмите любую клавишу для продолжения ");
                Console.ReadKey();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Выводит на экран текст главного меню с доступными действиями.
        /// </summary>
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("---- МЕНЮ ----");
            Console.WriteLine("1 - Создать машину");
            Console.WriteLine("2 - Удалить машину");
            Console.WriteLine("3 - Показать все машины");
            Console.WriteLine("4 - Изменить машину");
            Console.WriteLine("5 - Группировка машин по марке");
            Console.WriteLine("6 - Показать машины определенного года");
            Console.WriteLine("7 - Показать машины по цвету");
            Console.WriteLine("8 - Секретные функции");
            Console.WriteLine("0 - Выход");
        }

        /// <summary>
        /// Запрашивает у пользователя данные новой машины и создаёт её через Logic.
        /// </summary>
        static void AddCar()
        {
            Console.Write("Введите марку машины: ");
            string brand = Console.ReadLine() ?? "";
            Console.Write("Введите модель машины: ");
            string model = Console.ReadLine() ?? "";
            Console.Write("Введите цвет машины: ");
            string color = Console.ReadLine() ?? "";
            int year = ReadInt("Введите год выпуска машины: ");

            // Пробег генерируется случайно внутри CreateCar
            var car = logic.CreateCar(brand, model, color, year);
            Console.WriteLine($"Машина создана: {car}");
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает ID машины у пользователя и удаляет её, если она найдена.
        /// </summary>
        static void DeleteCar()
        {
            if (NoCars()) return;

            int id = ReadInt("Введите ID машины для удаления: ");
            bool deleted = logic.DeleteCar(id);

            if (deleted)
                Console.WriteLine($"Машина с ID {id} удалена.");
            else
                Console.WriteLine($"Машина с ID {id} не найдена.");
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит на экран список всех машин.
        /// </summary>
        static void AllCars()
        {
            if (NoCars()) return;
            var cars = logic.AllCars();
            Console.WriteLine("Все машины:");
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает ID машины и новые данные, обновляет машину, если она найдена.
        /// </summary>
        static void UpdateCar()
        {
            if (NoCars()) return;

            int id = ReadInt("Введите ID машины для изменения: ");
            var car = logic.CarId(id);

            if (car == null)
            {
                Console.WriteLine($"Машина с ID {id} не найдена.");
                Console.WriteLine("Нажмите любую клавишу для продолжения ");
                Console.ReadKey();
                return;
            }

            Console.Write("Введите новую марку машины: ");
            string brand = Console.ReadLine() ?? "";
            Console.Write("Введите новую модель машины: ");
            string model = Console.ReadLine() ?? "";
            Console.Write("Введите новый цвет машины: ");
            string color = Console.ReadLine() ?? "";
            int year = ReadInt("Введите новый год выпуска машины: ");

            bool updated = logic.UpdateCar(id, brand, model, color, year);
            if (updated)
            {
                Console.WriteLine($"Машина с ID {id} обновлена.");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Выводит машины, сгруппированные по бренду (марке).
        /// </summary>
        static void GroupBrand()
        {
            if (NoCars()) return;

            var carsByBrand = logic.CarsBrand();
            Console.WriteLine("Группировка машин по бренду:");

            foreach (var brand in carsByBrand.Keys)
            {
                Console.WriteLine($"Бренд: {brand}");
                foreach (var car in carsByBrand[brand])
                {
                    Console.WriteLine(car);
                }
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает год у пользователя и выводит машины этого года выпуска.
        /// </summary>
        static void CarsYear()
        {
            if (NoCars()) return;

            AllCars();
            int year = ReadInt("Введите год: ");

            var cars = logic.CarsYear(year);

            if (cars.Count == 0)
            {
                Console.WriteLine($"Машин {year} года не найдено");
                Console.WriteLine("Нажмите любую клавишу для продолжения ");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Машины {year} года:");
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает цвет у пользователя и выводит машины этого цвета.
        /// </summary>
        static void CarsColor()
        {
            if (NoCars()) return;

            Console.Write("Введите цвет: ");
            string color = Console.ReadLine() ?? "";
            var cars = logic.CarsColor(color);

            if (cars.Count == 0)
            {
                Console.WriteLine("Машин с таким цветом не найдено");
                Console.WriteLine("Нажмите любую клавишу для продолжения ");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Машины цвета   {color} :");
            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Показывает подменю с дополнительными действиями (тонировка, скрутка пробега)
        /// </summary>
        static void SecretFunctions()
        {
            Console.WriteLine("Заходя сюда вы возможно нарушите какой-либо закон;)");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1 - Добавить тонировку");
                Console.WriteLine("2 - Скрутить пробег");
                Console.WriteLine("0 - Назад");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        AddTinting();
                        break;
                    case "2":
                        RollBackMileage();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Console.WriteLine("Нажмите любую клавишу для продолжения ");
                        Console.ReadKey();
                        break;
                }
            }
        }

        /// <summary>
        /// Запрашивает ID машины и включает у неё тонировку окон.
        /// </summary>
        static void AddTinting()
        {
            if (NoCars()) return;

            AllCars();
            int id = ReadInt("Введите ID машины : ");

            bool added = logic.AddTinting(id);
            if (added)
                Console.WriteLine($"Тонировка добавлена для машины с ID {id}.");
            else
                Console.WriteLine($"Машина с ID {id} не найдена.");

            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает ID машины и новый пробег, устанавливает его,
        /// если новое значение меньше текущего 
        /// </summary>
        static void RollBackMileage()
        {
            if (NoCars()) return;

            AllCars();
            int id = ReadInt("Введите ID машины : ");

            var car = logic.CarId(id);
            if (car == null)
            {
                Console.WriteLine($"Машина с ID {id} не найдена.");
                Console.WriteLine("Нажмите любую клавишу для продолжения ");
                Console.ReadKey();
                return;
            }

            int newMileage = ReadInt("Введите новый пробег: ");
            bool rolled = logic.RollBackMileage(id, newMileage);

            if (rolled)
                Console.WriteLine($"Пробег машины с ID {id} скручен до {newMileage} км.");
            else
                Console.WriteLine("Новый пробег должен быть меньше текущего.");

            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }
    }
}
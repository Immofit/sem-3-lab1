using System;
using System.Collections.Generic;
using Shared;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное представление. Читает ввод, вызывает события и выводит данные на экран.
    /// </summary>
    public class ConsoleView : IView
    {
        public event Action? RefreshRequested;
        public event Action<CarDto>? CreateRequested;
        public event Action<CarDto>? UpdateRequested;
        public event Action<int>? DeleteRequested;
        public event Action? BrandGroupingRequested;
        public event Action<int>? FindByYearRequested;
        public event Action<string>? FindByColorRequested;
        public event Action<int>? AddTintingRequested;
        public event Action<int, int>? RollBackMileageRequested;



        /// <summary>
        /// Выводит список машин или сообщение, что список пуст.
        /// </summary>
        public void ShowCars(List<CarDto> cars)
        {
            if (cars.Count == 0)
            {
                Console.WriteLine("Список машин пуст.");
                return;
            }

            Console.WriteLine("Машины:");
            foreach (var car in cars)
            {
                Console.WriteLine(CarToText(car));
            }
        }

        /// <summary>
        /// Выводит машины, сгруппированные по бренду.
        /// </summary>
        public void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups)
        {
            if (groups.Count == 0)
            {
                Console.WriteLine("Список машин пуст.");
                return;
            }

            Console.WriteLine("Группировка машин по бренду:");

            foreach (var brand in groups.Keys)
            {
                Console.WriteLine($"Бренд: {brand}");
                foreach (var car in groups[brand])
                {
                    Console.WriteLine(CarToText(car));
                }
            }
        }

        /// <summary>
        /// Выводит обычное сообщение.
        /// </summary>
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Выводит сообщение об ошибке красным цветом.
        /// </summary>
        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        /// <summary>
        /// Собирает строку с данными машины для вывода в консоль.
        /// </summary>
        private string CarToText(CarDto car)
        {
            return $"Id={car.Id} | {car.Brand} {car.Model} | Цвет: {car.Color} | {car.Year} год | Пробег: {car.Mileage} км | Тонировка: {car.WindowTinting}";
        }



        /// <summary>
        /// Показывает меню и обрабатывает выбор пользователя в цикле.
        /// </summary>
        public void Run()
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
                        Pause();
                        break;
                }
            }
        }

        /// <summary>
        /// Ждёт нажатия любой клавиши.
        /// </summary>
        private void Pause()
        {
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        /// <summary>
        /// Запрашивает у пользователя целое число и повторяет запрос, пока ввод некорректный.
        /// </summary>
        private int ReadInt(string prompt)
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
        /// Запрашивает у пользователя непустую строку и повторяет запрос, пока ввод пустой.
        /// </summary>
        private string ReadNonEmptyString(string prompt, string errorMessage)
        {
            Console.Write(prompt);
            string input = Console.ReadLine() ?? "";

            while (input == "")
            {
                Console.WriteLine(errorMessage);
                Console.Write(prompt);
                input = Console.ReadLine() ?? "";
            }

            return input;
        }

        /// <summary>
        /// Выводит на экран текст главного меню с доступными действиями.
        /// </summary>
        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("---- МЕНЮ ----");
            Console.WriteLine("1 - Создать машину");
            Console.WriteLine("2 - Удалить машину");
            Console.WriteLine("3 - Показать все машины");
            Console.WriteLine("4 - Изменить машину");
            Console.WriteLine("5 - Группировка машин по бренду");
            Console.WriteLine("6 - Показать машины определенного года");
            Console.WriteLine("7 - Показать машины по цвету");
            Console.WriteLine("8 - Секретные функции");
            Console.WriteLine("0 - Выход");
        }

        /// <summary>
        /// Запрашивает у пользователя данные новой машины и просит Presenter её создать.
        /// </summary>
        private void AddCar()
        {
            CarDto car = new CarDto();
            car.Brand = ReadNonEmptyString("Введите бренд машины: ", "Бренд не может быть пустым.");
            car.Model = ReadNonEmptyString("Введите модель машины: ", "Модель не может быть пустой.");
            car.Color = ReadNonEmptyString("Введите цвет машины: ", "Цвет не может быть пустым.");
            car.Year = ReadInt("Введите год выпуска машины: ");

            CreateRequested?.Invoke(car);

            Pause();
        }

        /// <summary>
        /// Запрашивает ID машины у пользователя и просит Presenter её удалить.
        /// </summary>
        private void DeleteCar()
        {
            int id = ReadInt("Введите ID машины для удаления: ");

            DeleteRequested?.Invoke(id);

            Pause();
        }

        /// <summary>
        /// Просит Presenter показать список всех машин.
        /// </summary>
        private void AllCars()
        {
            RefreshRequested?.Invoke();

            Pause();
        }

        /// <summary>
        /// Запрашивает ID машины и новые данные и просит Presenter изменить машину.
        /// </summary>
        private void UpdateCar()
        {
            int id = ReadInt("Введите ID машины для изменения: ");

            CarDto car = new CarDto();
            car.Id = id;
            car.Brand = ReadNonEmptyString("Введите новый бренд машины: ", "Бренд не может быть пустым.");
            car.Model = ReadNonEmptyString("Введите новую модель машины: ", "Модель не может быть пустой.");
            car.Color = ReadNonEmptyString("Введите новый цвет машины: ", "Цвет не может быть пустым.");
            car.Year = ReadInt("Введите новый год выпуска машины: ");

            UpdateRequested?.Invoke(car);

            Pause();
        }

        /// <summary>
        /// Просит Presenter показать машины, сгруппированные по бренду.
        /// </summary>
        private void GroupBrand()
        {
            BrandGroupingRequested?.Invoke();

            Pause();
        }

        /// <summary>
        /// Показывает все машины, запрашивает год и просит Presenter найти машины этого года.
        /// </summary>
        private void CarsYear()
        {
            RefreshRequested?.Invoke();
            int year = ReadInt("Введите год: ");

            FindByYearRequested?.Invoke(year);

            Pause();
        }

        /// <summary>
        /// Запрашивает цвет у пользователя и просит Presenter найти машины этого цвета.
        /// </summary>
        private void CarsColor()
        {
            string color = ReadNonEmptyString("Введите цвет: ", "Цвет не может быть пустым.");

            FindByColorRequested?.Invoke(color);

            Pause();
        }

        /// <summary>
        /// Показывает подменю с дополнительными действиями (тонировка, скрутка пробега).
        /// </summary>
        private void SecretFunctions()
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
                        Pause();
                        break;
                }
            }
        }

        /// <summary>
        /// Показывает все машины, запрашивает ID и просит Presenter включить тонировку.
        /// </summary>
        private void AddTinting()
        {
            RefreshRequested?.Invoke();
            int id = ReadInt("Введите ID машины : ");

            AddTintingRequested?.Invoke(id);

            Pause();
        }

        /// <summary>
        /// Показывает все машины, запрашивает ID и новый пробег и просит Presenter скрутить пробег.
        /// </summary>
        private void RollBackMileage()
        {
            RefreshRequested?.Invoke();
            int id = ReadInt("Введите ID машины : ");
            int newMileage = ReadInt("Введите новый пробег: ");

            RollBackMileageRequested?.Invoke(id, newMileage);

            Pause();
        }
    }
}
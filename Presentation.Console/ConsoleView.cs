using System;
using System.Collections.Generic;
using Shared;

namespace ConsoleApp
{
    /// <summary>
    /// Консольное представление. Читает ввод, генерирует события и выводит данные на экран.
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

        // ---------- Методы IView: отрисовка ----------

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
                Console.WriteLine(car);
            }
        }

        public void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups)
        {
            if (groups.Count == 0)
            {
                Console.WriteLine("Список машин пуст.");
                return;
            }

            Console.WriteLine("Группировка машин по бренду:");
            foreach (var group in groups)
            {
                Console.WriteLine($"Бренд: {group.Key}");
                foreach (var car in group.Value)
                {
                    Console.WriteLine(car);
                }
            }
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        // ---------- Главный цикл ----------

        /// <summary>
        /// Показывает меню и обрабатывает выбор пользователя в цикле.
        /// </summary>
        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Menu();
                Console.Write("Введите ваш выбор: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": AddCar(); break;
                    case "2": DeleteCar(); break;
                    case "3": ShowAll(); break;
                    case "4": UpdateCar(); break;
                    case "5": GroupBrand(); break;
                    case "6": CarsYear(); break;
                    case "7": CarsColor(); break;
                    case "8": SecretFunctions(); break;
                    case "0": exit = true; break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        Pause();
                        break;
                }
            }
        }

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

        private void Pause()
        {
            Console.WriteLine("Нажмите любую клавишу для продолжения ");
            Console.ReadKey();
        }

        // ---------- Ввод ----------

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
        /// Запрашивает данные машины. Id передаётся, чтобы метод подходил и для создания, и для изменения.
        /// </summary>
        private CarDto ReadCar(int id)
        {
            return new CarDto
            {
                Id = id,
                Brand = ReadNonEmptyString("Введите бренд машины: ", "Бренд не может быть пустым."),
                Model = ReadNonEmptyString("Введите модель машины: ", "Модель не может быть пустой."),
                Color = ReadNonEmptyString("Введите цвет машины: ", "Цвет не может быть пустым."),
                Year = ReadInt("Введите год выпуска машины: ")
            };
        }

        // ---------- Пункты меню: только ввод и вызов события ----------

        private void AddCar()
        {
            CreateRequested?.Invoke(ReadCar(0));
            Pause();
        }

        private void DeleteCar()
        {
            int id = ReadInt("Введите ID машины для удаления: ");
            DeleteRequested?.Invoke(id);
            Pause();
        }

        private void ShowAll()
        {
            RefreshRequested?.Invoke();
            Pause();
        }

        private void UpdateCar()
        {
            int id = ReadInt("Введите ID машины для изменения: ");
            UpdateRequested?.Invoke(ReadCar(id));
            Pause();
        }

        private void GroupBrand()
        {
            BrandGroupingRequested?.Invoke();
            Pause();
        }

        private void CarsYear()
        {
            RefreshRequested?.Invoke();
            int year = ReadInt("Введите год: ");
            FindByYearRequested?.Invoke(year);
            Pause();
        }

        private void CarsColor()
        {
            string color = ReadNonEmptyString("Введите цвет: ", "Цвет не может быть пустым.");
            FindByColorRequested?.Invoke(color);
            Pause();
        }

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
                        RefreshRequested?.Invoke();
                        AddTintingRequested?.Invoke(ReadInt("Введите ID машины : "));
                        Pause();
                        break;
                    case "2":
                        RefreshRequested?.Invoke();
                        int id = ReadInt("Введите ID машины : ");
                        int mileage = ReadInt("Введите новый пробег: ");
                        RollBackMileageRequested?.Invoke(id, mileage);
                        Pause();
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
    }
}
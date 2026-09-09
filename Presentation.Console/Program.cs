using System;
using Model;
namespace ConsoleApp
{
    class Program
    {
        static Logic logic = new Logic();
        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Menu();
                Console.Write($"Введите ваш выбор: ");
                string choice = Console.ReadLine();
                {
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
                        case "0":
                            exit = true;
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
                static void Menu()//Меню
                {
                    Console.WriteLine("---- МЕНЮ ----");
                    Console.WriteLine("1 - Создать машину");
                    Console.WriteLine("2 - Удалить машину");
                    Console.WriteLine("3 - Показать все машины");
                    Console.WriteLine("4 - Изменить машину");
                    Console.WriteLine("5 - Группировка машин по марке");
                    Console.WriteLine("6 - Показать машины определенного года");
                    Console.WriteLine("7 - Показать машины по цвету");
                    
                    Console.WriteLine("0 - Выход");
                    Console.Write("Ваш выбор: ");
                }

                static void AddCar()//Добавить Машину
                {
                    Console.Clear();
                    Console.Write("Введите марку машины: ");
                    string brand = Console.ReadLine();
                    Console.Write("Введите модель машины: ");
                    string model = Console.ReadLine();
                    Console.Write("Введите цвет машины: ");
                    string color = Console.ReadLine();
                    Console.Write("Введите год выпуска машины: ");
                    int year = int.Parse(Console.ReadLine());
                    Console.Write("Введите пробег машины: ");
                    int mileage = int.Parse(Console.ReadLine());
                   
                    var car = logic.CreateCar(brand, model, color, year, mileage);
                    Console.WriteLine("Машина создана: " + car);
                }

                static void DeleteCar()//Удалить Машину
                {
                    Console.Clear();
                    Console.Write("Введите ID машины для удаления: ");
                    int id = int.Parse(Console.ReadLine());
                    bool deleted = logic.DeleteCar(id);
                    if (deleted)
                    {
                        Console.WriteLine("Машина с ID " + id + " удалена.");
                    }
                    else
                    {
                        Console.WriteLine("Машина с ID " + id + " не найдена.");
                    }
                }

                static void AllCars()//Все машины
                {
                    Console.Clear();
                    var cars = logic.AllCars();
                    Console.WriteLine("Все машины:");
                    foreach (var car in cars)
                    {
                        Console.WriteLine(car);
                    }
                }

                static void UpdateCar()//Изменить машину
                {
                    Console.Clear();
                    Console.Write("Введите ID машины для изменения: ");
                    int id = int.Parse(Console.ReadLine());
                    var car = logic.CarId(id);
                    if (car == null)
                    {
                        Console.WriteLine("Машина с ID " + id + " не найдена.");
                        return;
                    }
                    Console.Write("Введите новую марку машины: ");
                    string brand = Console.ReadLine();
                    Console.Write("Введите новую модель машины: ");
                    string model = Console.ReadLine();
                    Console.Write("Введите новый цвет машины: ");
                    string color = Console.ReadLine();
                    Console.Write("Введите новый год выпуска машины: ");
                    int year = int.Parse(Console.ReadLine());
                    bool updated = logic.UpdateCar(id, brand, model, color, year);
                    if (updated)
                    {
                        Console.WriteLine("Машина с ID " + id + " обновлена.");
                    }
                    
                }

                static void GroupBrand()//Группировка машин по бренду
                {
                    Console.Clear();
                    var carsByBrand = logic.CarsBrand();
                    Console.WriteLine("Группировка машин по бренду:");
                    foreach (var brand in carsByBrand.Keys)
                    {
                        Console.WriteLine("Бренд: " + brand);
                        foreach (var car in carsByBrand[brand])
                        {
                            Console.WriteLine(car);
                        }
                    }
                }

                static void CarsYear()//Показать машины определенного года
                {
                    Console.Clear();
                    Console.Write("Введите год: ");
                    int year = int.Parse(Console.ReadLine());

                    var cars = logic.CarsYear(year);

                    if (cars.Count == 0)
                    {
                        Console.WriteLine("Машин с таким годом не найдено");
                        return;
                    }

                    Console.WriteLine("Машины " + year + " года:");
                    foreach (var car in cars)
                    {
                        Console.WriteLine(car);
                    }
                }
            }
        }
    }
}
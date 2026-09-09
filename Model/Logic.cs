using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace Model
{
    public class Logic
    {
        private List<Car> cars;
        private int nextId;
        private static Random random = new Random();

        public Logic()
        {
            cars = new List<Car>();
            nextId = 1;
        }

        /// <summary>
        /// Создаёт машину со случайным пробегом и добавляет её в список.
        /// </summary>
        /// <param name="brand">Марка автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="color">Цвет автомобиля.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>Созданный объект машины.</returns>
        public Car CreateCar(string brand, string model, string color, int year)
        {
            int mileage = random.Next(1000, 200000);
            var car = new Car(nextId++, brand, model, color, year, mileage);
            cars.Add(car);
            return car;
        }

        /// <summary>
        /// Удаляет машину по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и удалена; иначе false.</returns>
        public bool DeleteCar(int id)
        {
            Car? car = null; // <- добавлен ? — переменная может быть null
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id)
                {
                    car = cars[i];
                    break;
                }
            }
            if (car != null)
            {
                cars.Remove(car);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Возвращает список всех машин.
        /// </summary>
        /// <returns>Список всех машин.</returns>
        public List<Car> AllCars()
        {
            return cars;
        }

        /// <summary>
        /// Ищет машину по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Найденная машина или null, если не найдена.</returns>
        public Car? CarId(int id) // <- добавлен ? — метод может вернуть null
        {
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id)
                {
                    return cars[i];
                }
            }
            return null;
        }

        /// <summary>
        /// Изменяет данные машины по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="brand">Новая марка.</param>
        /// <param name="model">Новая модель.</param>
        /// <param name="color">Новый цвет.</param>
        /// <param name="year">Новый год выпуска.</param>
        /// <returns>true, если машина найдена и обновлена; иначе false.</returns>
        public bool UpdateCar(int id, string brand, string model, string color, int year)
        {
            Car? car = null; // <- добавлен ?
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Id == id)
                {
                    car = cars[i];
                    break;
                }
            }
            if (car != null)
            {
                car.Brand = brand;
                car.Model = model;
                car.Color = color;
                car.Year = year;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Группирует все машины по бренду (марке).
        /// </summary>
        /// <returns>Словарь, где ключ — бренд, значение — список машин этого бренда.</returns>
        public Dictionary<string, List<Car>> CarsBrand()
        {
            var carsByBrand = new Dictionary<string, List<Car>>();
            foreach (var car in cars)
            {
                if (!carsByBrand.ContainsKey(car.Brand))
                {
                    carsByBrand[car.Brand] = new List<Car>();
                }
                carsByBrand[car.Brand].Add(car);
            }
            return carsByBrand;
        }

        /// <summary>
        /// Возвращает список машин указанного года выпуска.
        /// </summary>
        /// <param name="year">Год выпуска для поиска.</param>
        /// <returns>Список машин с указанным годом выпуска.</returns>
        public List<Car> CarsYear(int year)
        {
            List<Car> result = new List<Car>();

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Year == year)
                {
                    result.Add(cars[i]);
                }
            }

            return result;
        }

        /// <summary>
        /// Возвращает список машин указанного цвета (без учёта регистра).
        /// </summary>
        /// <param name="color">Цвет для поиска.</param>
        /// <returns>Список машин с указанным цветом.</returns>
        public List<Car> CarsColor(string color)
        {
            List<Car> result = new List<Car>();
            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(cars[i]);
                }
            }
            return result;
        }
    }
}
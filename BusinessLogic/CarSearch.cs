using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DataAccessLayer;

namespace BusinessLogic
{
    public class CarSearch
    {
        private IRepository<Car> repository;

        /// <summary>
        /// Создаёт объект для группировки и поиска машин.
        /// </summary>
        /// <param name="repository">Репозиторий, из которого читаются машины.</param>
        public CarSearch(IRepository<Car> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Группирует все машины по бренду.
        /// </summary>
        /// <returns>Словарь, где ключ — бренд, значение — список машин этого бренда.</returns>
        public Dictionary<string, List<Car>> CarsBrand()
        {
            var carsByBrand = new Dictionary<string, List<Car>>();
            foreach (var car in repository.ReadAll())
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

            foreach (var car in repository.ReadAll())
            {
                if (car.Year == year)
                {
                    result.Add(car);
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

            foreach (var car in repository.ReadAll())
            {
                if (car.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(car);
                }
            }

            return result;
        }
    }
}

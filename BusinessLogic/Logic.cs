using Model;
using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic
{
    public class Logic
    {
        /// <summary>
        /// Репозиторий, в котором хранятся машины. Сейчас работает через Entity Framework.
        /// Чтобы переключиться на Dapper, раскомментируйте вторую строку и закомментируйте первую.
        /// </summary>
        //private IRepository<Car> repository = new EntityRepository<Car>(new AppDbContext<Car>());
        private IRepository<Car> repository = new DapperRepository<Car>(DbSettings.ConnectionString);

        private static Random random = new Random();

        /// <summary>
        /// Создаёт машину со случайным пробегом и добавляет её в репозиторий.
        /// </summary>
        /// <param name="brand">Бренд автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="color">Цвет автомобиля.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>Созданный объект машины или null, если данные некорректны.</returns>
        public Car? CreateCar(string brand, string model, string color, int year)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(model))
            {
                return null;
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                return null;
            }
            if (year < 1900 || year > DateTime.Now.Year)
            {
                return null;
            }

            int mileage = random.Next(1000, 200000);
            
            var car = new Car(0, brand, model, color, year, mileage);
            repository.Add(car);
            return car;
        }

        /// <summary>
        /// Удаляет машину по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и удалена; иначе false.</returns>
        public bool DeleteCar(int id)
        {
            return repository.Delete(id);
        }

        /// <summary>
        /// Возвращает список всех машин.
        /// </summary>
        /// <returns>Список всех машин.</returns>
        public List<Car> AllCars()
        {
            return repository.ReadAll();
        }

        /// <summary>
        /// Ищет машину по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Найденная машина или null, если не найдена.</returns>
        public Car? CarId(int id)
        {
            return repository.ReadById(id);
        }

        /// <summary>
        /// Изменяет данные машины по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="brand">Новый бренд.</param>
        /// <param name="model">Новая модель.</param>
        /// <param name="color">Новый цвет.</param>
        /// <param name="year">Новый год выпуска.</param>
        /// <returns>true, если машина найдена, данные корректны и обновление прошло успешно; иначе false.</returns>
        public bool UpdateCar(int id, string brand, string model, string color, int year)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(model))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                return false;
            }
            if (year < 1900 || year > DateTime.Now.Year)
            {
                return false;
            }

            Car? car = repository.ReadById(id);
            if (car == null)
            {
                return false;
            }

            car.Brand = brand;
            car.Model = model;
            car.Color = color;
            car.Year = year;
            return repository.Update(car);
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

        /// <summary>
        /// Включает тонировку окон у машины с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и тонировка добавлена; иначе false.</returns>
        public bool AddTinting(int id)
        {
            var car = CarId(id);
            if (car != null)
            {
                car.WindowTinting = true;
                return repository.Update(car);
            }
            return false;
        }

        /// <summary>
        /// Скручивает пробег машины до нового значения, если оно меньше текущего.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="newMileage">Новое значение пробега.</param>
        /// <returns>true, если машина найдена и пробег успешно скручен; иначе false.</returns>
        public bool RollBackMileage(int id, int newMileage)
        {
            var car = CarId(id);
            if (car != null && newMileage >= 0 && newMileage < car.Mileage)
            {
                car.Mileage = newMileage;
                return repository.Update(car);
            }
            return false;
        }
    }
}

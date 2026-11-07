using DataAccessLayer;
using Model;
using Shared;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class Logic : ILogic
    {

        private IRepository<Car> repository;
        private static Random random = new Random();

        /// <summary>
        /// Возникает после любого успешного изменения списка машин.
        /// </summary>
        public event EventHandler? CarsChanged;

        public Logic(IRepository<Car> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Сообщает подписчикам (Presenter), что данные изменились.
        /// </summary>
        private void OnCarsChanged()
        {
            CarsChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Создаёт машину со случайным пробегом и добавляет её в репозиторий.
        /// </summary>
        /// <param name="brand">Бренд автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="color">Цвет автомобиля.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>Созданный объект машины или null, если данные некорректны.</returns>
        public CarDto? CreateCar(string brand, string model, string color, int year)
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
            OnCarsChanged();
            return CarMapper.ToDto(car);
        }

        /// <summary>
        /// Удаляет машину по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и удалена; иначе false.</returns>
        public bool DeleteCar(int id)
        {
            bool deleted = repository.Delete(id);
            if (deleted)
            {
                OnCarsChanged();
            }
            return deleted;
        }

        /// <summary>
        /// Возвращает список всех машин.
        /// </summary>
        /// <returns>Список всех машин.</returns>
        public List<CarDto> AllCars()
        {
            return repository.ReadAll().Select(CarMapper.ToDto).ToList();
        }

        /// <summary>
        /// Ищет машину по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Найденная машина или null, если не найдена.</returns>
        public CarDto? CarId(int id)
        {
            Car? car = repository.ReadById(id);
            if (car == null)
            {
                return null;
            }
            return CarMapper.ToDto(car);
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

            bool updated = repository.Update(car);
            if (updated)
            {
                OnCarsChanged();
            }
            return updated;
        }

        /// <summary>
        /// Группирует все машины по бренду.
        /// </summary>
        /// <returns>Словарь, где ключ — бренд, значение — список машин этого бренда.</returns>
        public Dictionary<string, List<CarDto>> CarsBrand()
        {
            var carsByBrand = new Dictionary<string, List<CarDto>>();
            foreach (var car in repository.ReadAll())
            {
                if (!carsByBrand.ContainsKey(car.Brand))
                {
                    carsByBrand[car.Brand] = new List<CarDto>();
                }
                carsByBrand[car.Brand].Add(CarMapper.ToDto(car));
            }
            return carsByBrand;
        }

        /// <summary>
        /// Возвращает список машин указанного года выпуска.
        /// </summary>
        /// <param name="year">Год выпуска для поиска.</param>
        /// <returns>Список машин с указанным годом выпуска.</returns>
        public List<CarDto> CarsYear(int year)
        {
            return repository.ReadAll()
                .Where(car => car.Year == year)
                .Select(CarMapper.ToDto)
                .ToList();
        }

        /// <summary>
        /// Возвращает список машин указанного цвета (без учёта регистра).
        /// </summary>
        /// <param name="color">Цвет для поиска.</param>
        /// <returns>Список машин с указанным цветом.</returns>
        public List<CarDto> CarsColor(string color)
        {
            return repository.ReadAll()
                .Where(car => car.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
                .Select(CarMapper.ToDto)
                .ToList();
        }

        /// <summary>
        /// Включает тонировку окон у машины с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и тонировка добавлена; false, если машина не найдена или тонировка уже стоит.</returns>
        public bool AddTinting(int id)
        {
            Car? car = repository.ReadById(id);
            if (car == null || car.WindowTinting)
            {
                return false;
            }

            car.WindowTinting = true;
            bool updated = repository.Update(car);
            if (updated)
            {
                OnCarsChanged();
            }
            return updated;
        }

        /// <summary>
        /// Скручивает пробег машины до нового значения, если оно меньше текущего.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="newMileage">Новое значение пробега.</param>
        /// <returns>true, если машина найдена и пробег успешно скручен; иначе false.</returns>
        public bool RollBackMileage(int id, int newMileage)
        {
            Car? car = repository.ReadById(id);
            if (car == null || newMileage < 0 || newMileage >= car.Mileage)
            {
                return false;
            }

            car.Mileage = newMileage;
            bool updated = repository.Update(car);
            if (updated)
            {
                OnCarsChanged();
            }
            return updated;
        }
    }
}
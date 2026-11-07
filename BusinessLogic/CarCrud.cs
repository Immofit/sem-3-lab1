using Model;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer;

namespace BusinessLogic
{
    public class CarCrud
    {
        private IRepository<Car> repository;
        private CarValidator validator;
        private MileageGenerator mileageGenerator;

        /// <summary>
        /// Создаёт объект для добавления, чтения, изменения и удаления машин.
        /// </summary>
        /// <param name="repository">Репозиторий, через который ведётся работа.</param>
        /// <param name="validator">Проверяет данные машины перед сохранением.</param>
        /// <param name="mileageGenerator">Задаёт пробег новой машины.</param>
        public CarCrud(IRepository<Car> repository, CarValidator validator, MileageGenerator mileageGenerator)
        {
            this.repository = repository;
            this.validator = validator;
            this.mileageGenerator = mileageGenerator;
        }

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
            if (!validator.IsValid(brand, model, color, year))
            {
                return null;
            }

            int mileage = mileageGenerator.Generate();

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
            if (!validator.IsValid(brand, model, color, year))
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
    }
}

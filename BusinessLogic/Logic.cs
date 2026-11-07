using Model;
using Shared;
using System;
using System.Collections.Generic;
using DataAccessLayer;

namespace BusinessLogic
{
    public class Logic : ILogic
    {
        private CarCrud crud;
        private CarSearch search;
        private CarSecret secret;
        private CarMapper mapper;

        /// <summary>
        /// Возникает после каждого успешного изменения данных о машинах.
        /// </summary>
        public event EventHandler? CarsChanged;

        /// <summary>
        /// Создаёт бизнес-логику с указанным репозиторием и собирает из него рабочие классы.
        /// </summary>
        /// <param name="repository">Репозиторий, через который ведётся работа.</param>
        public Logic(IRepository<Car> repository)
        {
            crud = new CarCrud(repository, new CarValidator(), new MileageGenerator());
            search = new CarSearch(repository);
            secret = new CarSecret(repository);
            mapper = new CarMapper();
        }

        /// <summary>
        /// Вызывает событие CarsChanged, если на него кто-то подписан.
        /// </summary>
        protected virtual void OnCarsChanged()
        {
            if (CarsChanged != null)
            {
                CarsChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Создаёт машину со случайным пробегом и добавляет её в репозиторий.
        /// </summary>
        /// <param name="brand">Бренд автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="color">Цвет автомобиля.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>Созданная машина или null, если данные некорректны.</returns>
        public virtual CarDto? CreateCar(string brand, string model, string color, int year)
        {
            Car? car = crud.CreateCar(brand, model, color, year);
            if (car == null)
            {
                return null;
            }

            OnCarsChanged();
            return mapper.ToDto(car);
        }

        /// <summary>
        /// Удаляет машину по указанному идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и удалена; иначе false.</returns>
        public virtual bool DeleteCar(int id)
        {
            bool result = crud.DeleteCar(id);
            if (result)
            {
                OnCarsChanged();
            }
            return result;
        }

        /// <summary>
        /// Возвращает список всех машин.
        /// </summary>
        /// <returns>Список всех машин.</returns>
        public virtual List<CarDto> AllCars()
        {
            return mapper.ToDtoList(crud.AllCars());
        }

        /// <summary>
        /// Ищет машину по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Найденная машина или null, если не найдена.</returns>
        public virtual CarDto? CarId(int id)
        {
            Car? car = crud.CarId(id);
            if (car == null)
            {
                return null;
            }
            return mapper.ToDto(car);
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
        public virtual bool UpdateCar(int id, string brand, string model, string color, int year)
        {
            bool result = crud.UpdateCar(id, brand, model, color, year);
            if (result)
            {
                OnCarsChanged();
            }
            return result;
        }

        /// <summary>
        /// Группирует все машины по бренду.
        /// </summary>
        /// <returns>Словарь, где ключ — бренд, значение — список машин этого бренда.</returns>
        public virtual Dictionary<string, List<CarDto>> CarsBrand()
        {
            return mapper.ToDtoGroups(search.CarsBrand());
        }

        /// <summary>
        /// Возвращает список машин указанного года выпуска.
        /// </summary>
        /// <param name="year">Год выпуска для поиска.</param>
        /// <returns>Список машин с указанным годом выпуска.</returns>
        public virtual List<CarDto> CarsYear(int year)
        {
            return mapper.ToDtoList(search.CarsYear(year));
        }

        /// <summary>
        /// Возвращает список машин указанного цвета (без учёта регистра).
        /// </summary>
        /// <param name="color">Цвет для поиска.</param>
        /// <returns>Список машин с указанным цветом.</returns>
        public virtual List<CarDto> CarsColor(string color)
        {
            return mapper.ToDtoList(search.CarsColor(color));
        }

        /// <summary>
        /// Включает тонировку окон у машины с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и тонировка добавлена; false, если машина не найдена или тонировка уже стоит.</returns>
        public virtual bool AddTinting(int id)
        {
            bool result = secret.AddTinting(id);
            if (result)
            {
                OnCarsChanged();
            }
            return result;
        }

        /// <summary>
        /// Скручивает пробег машины до нового значения, если оно меньше текущего.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="newMileage">Новое значение пробега.</param>
        /// <returns>true, если машина найдена и пробег успешно скручен; иначе false.</returns>
        public virtual bool RollBackMileage(int id, int newMileage)
        {
            bool result = secret.RollBackMileage(id, newMileage);
            if (result)
            {
                OnCarsChanged();
            }
            return result;
        }
    }
}
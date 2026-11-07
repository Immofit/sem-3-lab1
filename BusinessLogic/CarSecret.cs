using System;
using System.Collections.Generic;
using System.Text;
using Model;
using DataAccessLayer;

namespace BusinessLogic
{
    public class CarSecret
    {
        private IRepository<Car> repository;

        /// <summary>
        /// Создаёт объект для скрытых операций над машиной: тонировка и пробег.
        /// </summary>
        /// <param name="repository">Репозиторий, через который ведётся работа.</param>
        public CarSecret(IRepository<Car> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Включает тонировку окон у машины с указанным идентификатором.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена и тонировка добавлена; false, если машина не найдена или тонировка уже стоит.</returns>
        public bool AddTinting(int id)
        {
            var car = repository.ReadById(id);
            if (car == null)
            {
                return false;
            }
            if (car.WindowTinting)
            {
                return false;
            }

            car.WindowTinting = true;
            return repository.Update(car);
        }

        /// <summary>
        /// Скручивает пробег машины до нового значения, если оно меньше текущего.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="newMileage">Новое значение пробега.</param>
        /// <returns>true, если машина найдена и пробег успешно скручен; иначе false.</returns>
        public bool RollBackMileage(int id, int newMileage)
        {
            var car = repository.ReadById(id);
            if (car != null && newMileage >= 0 && newMileage < car.Mileage)
            {
                car.Mileage = newMileage;
                return repository.Update(car);
            }
            return false;
        }
    }
}

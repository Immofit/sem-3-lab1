using BusinessLogic;
using Shared;
using System;
using System.Collections.Generic;

namespace Presenter
{
    /// <summary>
    /// Presenter для работы с автомобилями: связывает представление <see cref="IView"/>
    /// и бизнес-логику <see cref="ILogic"/>, подписываясь на события обеих сторон.
    /// </summary>
    public class CarPresenter
    {
        private IView view;
        private ILogic logic;

        /// <summary>
        /// Создаёт Presenter и подписывает его на события представления и бизнес-логики.
        /// </summary>
        /// <param name="view">Представление, которое обслуживает Presenter.</param>
        /// <param name="logic">Бизнес-логика, с которой работает Presenter.</param>
        public CarPresenter(IView view, ILogic logic)
        {
            this.view = view;
            this.logic = logic;

            logic.CarsChanged += OnCarsChanged;

            view.RefreshRequested += OnRefreshRequested;
            view.CreateRequested += OnCreateRequested;
            view.UpdateRequested += OnUpdateRequested;
            view.DeleteRequested += OnDeleteRequested;
            view.BrandGroupingRequested += OnBrandGroupingRequested;
            view.FindByYearRequested += OnFindByYearRequested;
            view.FindByColorRequested += OnFindByColorRequested;
            view.AddTintingRequested += OnAddTintingRequested;
            view.RollBackMileageRequested += OnRollBackMileageRequested;
        }

        /// <summary>
        /// Обрабатывает изменение данных в бизнес-логике: передаёт в представление
        /// актуальный список всех машин.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void OnCarsChanged(object? sender, EventArgs e)
        {
            view.ShowCars(logic.AllCars());
        }

        /// <summary>
        /// Обрабатывает запрос на обновление: передаёт в представление список всех машин.
        /// </summary>
        private void OnRefreshRequested()
        {
            view.ShowCars(logic.AllCars());
        }

        /// <summary>
        /// Обрабатывает создание машины: вызывает бизнес-логику и сообщает представлению результат.
        /// </summary>
        /// <param name="dto">Данные создаваемой машины.</param>
        private void OnCreateRequested(CarDto dto)
        {
            CarDto? car = logic.CreateCar(dto.Brand, dto.Model, dto.Color, dto.Year);
            if (car == null)
            {
                view.ShowError("Машина не создана: данные некорректны.");
                return;
            }
            view.ShowMessage("Машина создана.");
        }

        /// <summary>
        /// Обрабатывает изменение машины: вызывает бизнес-логику и сообщает представлению результат.
        /// </summary>
        /// <param name="dto">Данные машины с новыми значениями полей.</param>
        private void OnUpdateRequested(CarDto dto)
        {
            if (logic.UpdateCar(dto.Id, dto.Brand, dto.Model, dto.Color, dto.Year))
            {
                view.ShowMessage("Данные машины изменены.");
            }
            else
            {
                view.ShowError("Данные машины не изменены: машина не найдена или данные некорректны.");
            }
        }

        /// <summary>
        /// Обрабатывает удаление машины: вызывает бизнес-логику и сообщает представлению результат.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой машины.</param>
        private void OnDeleteRequested(int id)
        {
            if (logic.DeleteCar(id))
            {
                view.ShowMessage("Машина удалена.");
            }
            else
            {
                view.ShowError("Машина не удалена: не найдена.");
            }
        }

        /// <summary>
        /// Обрабатывает запрос на группировку: передаёт в представление машины,
        /// сгруппированные по бренду.
        /// </summary>
        private void OnBrandGroupingRequested()
        {
            view.ShowCarsByBrand(logic.CarsBrand());
        }

        /// <summary>
        /// Обрабатывает поиск по году: передаёт в представление найденные машины
        /// или сообщение об их отсутствии.
        /// </summary>
        /// <param name="year">Год выпуска для поиска.</param>
        private void OnFindByYearRequested(int year)
        {
            List<CarDto> cars = logic.CarsYear(year);
            if (cars.Count == 0)
            {
                view.ShowMessage("Машин указанного года не найдено.");
                return;
            }
            view.ShowCars(cars);
        }

        /// <summary>
        /// Обрабатывает поиск по цвету: передаёт в представление найденные машины
        /// или сообщение об их отсутствии.
        /// </summary>
        /// <param name="color">Цвет для поиска.</param>
        private void OnFindByColorRequested(string color)
        {
            List<CarDto> cars = logic.CarsColor(color);
            if (cars.Count == 0)
            {
                view.ShowMessage("Машин указанного цвета не найдено.");
                return;
            }
            view.ShowCars(cars);
        }

        /// <summary>
        /// Обрабатывает добавление тонировки: вызывает бизнес-логику и сообщает представлению результат.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        private void OnAddTintingRequested(int id)
        {
            if (logic.AddTinting(id))
            {
                view.ShowMessage("Тонировка добавлена.");
            }
            else
            {
                view.ShowError("Тонировка не добавлена: машина не найдена или тонировка уже стоит.");
            }
        }

        /// <summary>
        /// Обрабатывает скручивание пробега: вызывает бизнес-логику и сообщает представлению результат.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <param name="newMileage">Новое (уменьшенное) значение пробега.</param>
        private void OnRollBackMileageRequested(int id, int newMileage)
        {
            if (logic.RollBackMileage(id, newMileage))
            {
                view.ShowMessage("Пробег скручен.");
            }
            else
            {
                view.ShowError("Пробег не скручен: машина не найдена или значение не меньше текущего.");
            }
        }
    }
}
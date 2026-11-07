using BusinessLogic;
using Shared;
using System;
using System.Collections.Generic;

namespace Presenter
{
    public class CarPresenter
    {
        private IView view;
        private ILogic logic;

        /// <summary>
        /// Связывает представление и бизнес-логику: подписывается на события обеих сторон.
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
        /// Данные изменились в бизнес-логике: передаёт в представление обновлённый список машин.
        /// </summary>
        private void OnCarsChanged(object? sender, EventArgs e)
        {
            view.ShowCars(logic.AllCars());
        }

        /// <summary>
        /// Передаёт в представление список всех машин.
        /// </summary>
        private void OnRefreshRequested()
        {
            view.ShowCars(logic.AllCars());
        }

        /// <summary>
        /// Создаёт машину и сообщает представлению результат.
        /// </summary>
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
        /// Изменяет машину и сообщает представлению результат.
        /// </summary>
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
        /// Удаляет машину и сообщает представлению результат.
        /// </summary>
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
        /// Передаёт в представление машины, сгруппированные по бренду.
        /// </summary>
        private void OnBrandGroupingRequested()
        {
            view.ShowCarsByBrand(logic.CarsBrand());
        }

        /// <summary>
        /// Передаёт в представление машины указанного года или сообщение, что их нет.
        /// </summary>
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
        /// Передаёт в представление машины указанного цвета или сообщение, что их нет.
        /// </summary>
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
        /// Добавляет тонировку и сообщает представлению результат.
        /// </summary>
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
        /// Скручивает пробег и сообщает представлению результат.
        /// </summary>
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
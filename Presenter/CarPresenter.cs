using System;
using System.Linq;
using BusinessLogic;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Обслуживает View: подписан на её события, вызывает ILogic,
    /// слушает события бизнес-логики и перерисовывает View через методы IView.
    /// </summary>
    public class CarPresenter
    {
        private IView view;
        private ILogic logic;

        /// <summary>
        /// Создаёт Presenter и подписывается на события View и бизнес-логики.
        /// </summary>
        /// <param name="view">Представление.</param>
        /// <param name="logic">Бизнес-логика (через интерфейс).</param>
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

        /// <summary>Данные изменились в бизнес-логике: перерисовываем список.</summary>
        private void OnCarsChanged(object? sender, EventArgs e)
        {
            view.ShowCars(logic.AllCars());
        }

        private void OnRefreshRequested()
        {
            view.ShowCars(logic.AllCars());
        }

        private void OnCreateRequested(CarDto dto)
        {
            CarDto? car = logic.CreateCar(dto.Brand, dto.Model, dto.Color, dto.Year);
            if (car == null)
            {
                view.ShowError("Не удалось создать машину. Поля не должны быть пустыми, год от 1900 до текущего.");
                return;
            }
            view.ShowMessage("Машина создана: " + car);
        }

        private void OnUpdateRequested(CarDto dto)
        {
            if (logic.CarId(dto.Id) == null)
            {
                view.ShowError("Машина с ID " + dto.Id + " не найдена.");
                return;
            }

            if (!logic.UpdateCar(dto.Id, dto.Brand, dto.Model, dto.Color, dto.Year))
            {
                view.ShowError("Не удалось изменить машину. Поля не должны быть пустыми, год от 1900 до текущего.");
                return;
            }
            view.ShowMessage("Машина с ID " + dto.Id + " обновлена.");
        }

        private void OnDeleteRequested(int id)
        {
            if (!logic.DeleteCar(id))
            {
                view.ShowError("Машина с ID " + id + " не найдена.");
                return;
            }
            view.ShowMessage("Машина с ID " + id + " удалена.");
        }

        private void OnBrandGroupingRequested()
        {
            view.ShowCarsByBrand(logic.CarsBrand());
        }

        private void OnFindByYearRequested(int year)
        {
            var cars = logic.CarsYear(year);
            if (cars.Count == 0)
            {
                view.ShowError("Машин " + year + " года не найдено.");
                return;
            }
            view.ShowCars(cars);
        }

        private void OnFindByColorRequested(string color)
        {
            var cars = logic.CarsColor(color);
            if (cars.Count == 0)
            {
                view.ShowError("Машин цвета " + color + " не найдено.");
                return;
            }
            view.ShowCars(cars);
        }

        private void OnAddTintingRequested(int id)
        {
            CarDto? car = logic.CarId(id);
            if (car == null)
            {
                view.ShowError("Машина с ID " + id + " не найдена.");
                return;
            }
            if (car.WindowTinting)
            {
                view.ShowError("Тонировка уже установлена на эту машину.");
                return;
            }

            if (logic.AddTinting(id))
            {
                view.ShowMessage("Тонировка добавлена для машины с ID " + id + ".");
            }
        }

        private void OnRollBackMileageRequested(int id, int newMileage)
        {
            if (logic.CarId(id) == null)
            {
                view.ShowError("Машина с ID " + id + " не найдена.");
                return;
            }

            if (!logic.RollBackMileage(id, newMileage))
            {
                view.ShowError("Новый пробег должен быть неотрицательным и меньше текущего.");
                return;
            }
            view.ShowMessage("Пробег машины с ID " + id + " скручен до " + newMileage + " км.");
        }
    }
}
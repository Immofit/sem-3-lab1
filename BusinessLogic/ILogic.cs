using System;
using System.Collections.Generic;
using Shared;

namespace BusinessLogic
{
    /// <summary>
    /// Фасад бизнес-логики для Presenter. Возвращает только DTO, доменный Car наружу не выходит.
    /// </summary>
    public interface ILogic
    {
        /// <summary>Возникает после любого успешного изменения списка машин.</summary>
        event EventHandler? CarsChanged;

        /// <summary>Создаёт машину. Возвращает null, если данные некорректны.</summary>
        CarDto? CreateCar(string brand, string model, string color, int year);

        /// <summary>Удаляет машину. false, если машины с таким Id нет.</summary>
        bool DeleteCar(int id);

        /// <summary>Возвращает все машины.</summary>
        List<CarDto> AllCars();

        /// <summary>Ищет машину по Id. null, если не найдена.</summary>
        CarDto? CarId(int id);

        /// <summary>Изменяет машину. false, если данные некорректны или машины нет.</summary>
        bool UpdateCar(int id, string brand, string model, string color, int year);

        /// <summary>Группирует машины по бренду.</summary>
        Dictionary<string, List<CarDto>> CarsBrand();

        /// <summary>Возвращает машины указанного года.</summary>
        List<CarDto> CarsYear(int year);

        /// <summary>Возвращает машины указанного цвета.</summary>
        List<CarDto> CarsColor(string color);

        /// <summary>Добавляет тонировку. false, если машины нет или тонировка уже стоит.</summary>
        bool AddTinting(int id);

        /// <summary>Скручивает пробег. false, если машины нет или значение некорректно.</summary>
        bool RollBackMileage(int id, int newMileage);
    }
}
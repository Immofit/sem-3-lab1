using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public interface ILogic
    {
        /// <summary>
        /// Возникает после каждого успешного изменения данных о машинах.
        /// </summary>
        event EventHandler? CarsChanged;

        /// <summary>
        /// Создаёт машину со случайным пробегом.
        /// </summary>
        /// <returns>Созданная машина или null, если данные некорректны.</returns>
        CarDto? CreateCar(string brand, string model, string color, int year);

        /// <summary>
        /// Удаляет машину по идентификатору.
        /// </summary>
        /// <returns>true, если машина удалена; иначе false.</returns>
        bool DeleteCar(int id);

        /// <summary>
        /// Возвращает все машины.
        /// </summary>
        /// <returns>Список всех машин.</returns>
        List<CarDto> AllCars();

        /// <summary>
        /// Ищет машину по идентификатору.
        /// </summary>
        /// <returns>Найденная машина или null.</returns>
        CarDto? CarId(int id);

        /// <summary>
        /// Изменяет данные машины.
        /// </summary>
        /// <returns>true, если машина изменена; иначе false.</returns>
        bool UpdateCar(int id, string brand, string model, string color, int year);

        /// <summary>
        /// Группирует машины по бренду.
        /// </summary>
        /// <returns>Словарь «бренд — список машин».</returns>
        Dictionary<string, List<CarDto>> CarsBrand();

        /// <summary>
        /// Возвращает машины указанного года выпуска.
        /// </summary>
        /// <returns>Список машин нужного года.</returns>
        List<CarDto> CarsYear(int year);

        /// <summary>
        /// Возвращает машины указанного цвета.
        /// </summary>
        /// <returns>Список машин нужного цвета.</returns>
        List<CarDto> CarsColor(string color);

        /// <summary>
        /// Включает тонировку окон у машины.
        /// </summary>
        /// <returns>true, если тонировка добавлена; иначе false.</returns>
        bool AddTinting(int id);

        /// <summary>
        /// Скручивает пробег машины до меньшего значения.
        /// </summary>
        /// <returns>true, если пробег скручен; иначе false.</returns>
        bool RollBackMileage(int id, int newMileage);
    }
}
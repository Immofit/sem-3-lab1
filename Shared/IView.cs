using System;
using System.Collections.Generic;

namespace Shared
{
    public interface IView
    {
        /// <summary>
        /// Пользователь просит показать все машины.
        /// </summary>
        event Action? RefreshRequested;

        /// <summary>
        /// Пользователь просит создать машину. Заполнены Brand, Model, Color, Year.
        /// </summary>
        event Action<CarDto>? CreateRequested;

        /// <summary>
        /// Пользователь просит изменить машину. Заполнены Id, Brand, Model, Color, Year.
        /// </summary>
        event Action<CarDto>? UpdateRequested;

        /// <summary>
        /// Пользователь просит удалить машину с указанным Id.
        /// </summary>
        event Action<int>? DeleteRequested;

        /// <summary>
        /// Пользователь просит сгруппировать машины по бренду.
        /// </summary>
        event Action? BrandGroupingRequested;

        /// <summary>
        /// Пользователь просит найти машины указанного года.
        /// </summary>
        event Action<int>? FindByYearRequested;

        /// <summary>
        /// Пользователь просит найти машины указанного цвета.
        /// </summary>
        event Action<string>? FindByColorRequested;

        /// <summary>
        /// Пользователь просит добавить тонировку машине с указанным Id.
        /// </summary>
        event Action<int>? AddTintingRequested;

        /// <summary>
        /// Пользователь просит скрутить пробег: первый параметр — Id машины, второй — новый пробег.
        /// </summary>
        event Action<int, int>? RollBackMileageRequested;

        /// <summary>
        /// Показывает список машин.
        /// </summary>
        /// <param name="cars">Список машин для отображения.</param>
        void ShowCars(List<CarDto> cars);

        /// <summary>
        /// Показывает машины, сгруппированные по бренду.
        /// </summary>
        /// <param name="groups">Словарь, где ключ — бренд, значение — список машин этого бренда.</param>
        void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups);

        /// <summary>
        /// Показывает пользователю информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        void ShowError(string message);
    }
}
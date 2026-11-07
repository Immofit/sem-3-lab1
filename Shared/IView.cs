using System;
using System.Collections.Generic;

namespace Shared
{
    /// <summary>
    /// Интерфейс представления. Presenter знает о View только через него.
    /// События View генерирует, методы показа вызывает Presenter.
    /// </summary>
    public interface IView
    {
        // ---------- События View (на них подписан Presenter) ----------

        /// <summary>Пользователь запросил показать все машины.</summary>
        event Action? RefreshRequested;

        /// <summary>Пользователь хочет создать машину (Id в DTO не используется).</summary>
        event Action<CarDto>? CreateRequested;

        /// <summary>Пользователь хочет изменить машину (Id берётся из DTO).</summary>
        event Action<CarDto>? UpdateRequested;

        /// <summary>Пользователь хочет удалить машину с указанным Id.</summary>
        event Action<int>? DeleteRequested;

        /// <summary>Пользователь запросил группировку машин по бренду.</summary>
        event Action? BrandGroupingRequested;

        /// <summary>Пользователь запросил поиск машин по году.</summary>
        event Action<int>? FindByYearRequested;

        /// <summary>Пользователь запросил поиск машин по цвету.</summary>
        event Action<string>? FindByColorRequested;

        /// <summary>Пользователь хочет добавить тонировку машине с указанным Id.</summary>
        event Action<int>? AddTintingRequested;

        /// <summary>Пользователь хочет скрутить пробег: Id машины и новое значение.</summary>
        event Action<int, int>? RollBackMileageRequested;

        // ---------- Методы отрисовки (их вызывает Presenter) ----------

        /// <summary>Показывает список машин.</summary>
        void ShowCars(List<CarDto> cars);

        /// <summary>Показывает машины, сгруппированные по бренду.</summary>
        void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups);

        /// <summary>Показывает пользователю информационное сообщение.</summary>
        void ShowMessage(string message);

        /// <summary>Показывает пользователю сообщение об ошибке.</summary>
        void ShowError(string message);
    }
}
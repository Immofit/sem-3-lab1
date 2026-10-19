using System;

namespace BusinessLogic
{
    /// <summary>
    /// Единое место, где описаны правила проверки данных машины.
    /// Используется и в Logic, и в слое View, чтобы правила не дублировались.
    /// </summary>
    public static class CarValidator
    {
        /// <summary>
        /// Минимально допустимый год выпуска.
        /// </summary>
        public const int MinYear = 1900;

        /// <summary>
        /// Проверяет, что год выпуска лежит в диапазоне от MinYear до текущего года.
        /// </summary>
        /// <param name="year">Год выпуска.</param>
        /// <returns>true, если год корректный; иначе false.</returns>
        public static bool IsValidYear(int year)
        {
            return year >= MinYear && year <= DateTime.Now.Year;
        }

        /// <summary>
        /// Проверяет все данные машины: строки не пустые, год корректный.
        /// </summary>
        /// <param name="brand">Бренд.</param>
        /// <param name="model">Модель.</param>
        /// <param name="color">Цвет.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>true, если все данные корректны; иначе false.</returns>
        public static bool IsValid(string brand, string model, string color, int year)
        {
            if (string.IsNullOrWhiteSpace(brand))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(model))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(color))
            {
                return false;
            }
            return IsValidYear(year);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class CarValidator
    {
        /// <summary>
        /// Проверяет, что бренд, модель и цвет не пустые, а год выпуска лежит между 1900 и текущим годом.
        /// </summary>
        /// <param name="brand">Бренд автомобиля.</param>
        /// <param name="model">Модель автомобиля.</param>
        /// <param name="color">Цвет автомобиля.</param>
        /// <param name="year">Год выпуска.</param>
        /// <returns>true, если все данные корректны; иначе false.</returns>
        public bool IsValid(string brand, string model, string color, int year)
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
            if (year < 1900 || year > DateTime.Now.Year)
            {
                return false;
            }
            return true;
        }
    }
}

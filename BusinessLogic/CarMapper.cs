using Model;
using Shared;

namespace BusinessLogic
{
    /// <summary>
    /// Преобразует доменный объект Car в DTO для передачи наверх.
    /// </summary>
    internal static class CarMapper
    {
        /// <summary>
        /// Создаёт DTO по доменному объекту.
        /// </summary>
        /// <param name="car">Доменный объект машины.</param>
        /// <returns>DTO с теми же данными.</returns>
        public static CarDto ToDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Color = car.Color,
                Year = car.Year,
                Mileage = car.Mileage,
                WindowTinting = car.WindowTinting
            };
        }
    }
}
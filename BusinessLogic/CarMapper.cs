using System;
using System.Collections.Generic;
using System.Text;
using Model;
using Shared;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class CarMapper
    {
        /// <summary>
        /// Переносит данные машины в объект для передачи в представление.
        /// </summary>
        /// <param name="car">Доменный объект машины.</param>
        /// <returns>Объект CarDto с теми же данными.</returns>
        public CarDto ToDto(Car car)
        {
            CarDto dto = new CarDto();
            dto.Id = car.Id;
            dto.Brand = car.Brand;
            dto.Model = car.Model;
            dto.Color = car.Color;
            dto.Year = car.Year;
            dto.Mileage = car.Mileage;
            dto.WindowTinting = car.WindowTinting;
            return dto;
        }

        /// <summary>
        /// Переносит данные списка машин в список объектов CarDto.
        /// </summary>
        /// <param name="cars">Список доменных объектов.</param>
        /// <returns>Список объектов CarDto в том же порядке.</returns>
        public List<CarDto> ToDtoList(List<Car> cars)
        {
            List<CarDto> result = new List<CarDto>();
            foreach (var car in cars)
            {
                result.Add(ToDto(car));
            }
            return result;
        }

        /// <summary>
        /// Переносит данные словаря «бренд — список машин» в словарь с объектами CarDto.
        /// </summary>
        /// <param name="groups">Словарь с доменными объектами.</param>
        /// <returns>Словарь с теми же ключами и списками CarDto.</returns>
        public Dictionary<string, List<CarDto>> ToDtoGroups(Dictionary<string, List<Car>> groups)
        {
            var result = new Dictionary<string, List<CarDto>>();
            foreach (var pair in groups)
            {
                result[pair.Key] = ToDtoList(pair.Value);
            }
            return result;
        }
    }
}
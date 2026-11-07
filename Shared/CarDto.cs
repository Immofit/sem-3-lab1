namespace Shared
{
    /// <summary>
    /// Объект передачи данных о машине между слоями View, Presenter и BusinessLogic.
    /// Содержит только данные, без логики.
    /// </summary>
    public class CarDto
    {
        /// <summary>Уникальный идентификатор автомобиля.</summary>
        public int Id { get; set; }

        /// <summary>Бренд автомобиля.</summary>
        public string Brand { get; set; } = "";

        /// <summary>Модель автомобиля.</summary>
        public string Model { get; set; } = "";

        /// <summary>Цвет кузова автомобиля.</summary>
        public string Color { get; set; } = "";

        /// <summary>Год выпуска автомобиля.</summary>
        public int Year { get; set; }

        /// <summary>Пробег автомобиля в километрах.</summary>
        public int Mileage { get; set; }

        /// <summary>Признак наличия тонировки окон.</summary>
        public bool WindowTinting { get; set; }

        /// <summary>
        /// Возвращает строковое представление машины для вывода на экран.
        /// </summary>
        public override string ToString()
        {
            return "Id=" + Id + " | " + Brand + " " + Model + " | Цвет: " + Color + " | " + Year + " год | Пробег: " + Mileage + " км | Тонировка: " + WindowTinting;
        }
    }
}
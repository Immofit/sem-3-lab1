/// <summary>
/// Представляет автомобиль с основными характеристиками:
/// идентификатором, ,брендом, моделью, цветом, годом выпуска, пробегом
/// и наличием тонировки окон.
/// </summary>
public class Car
{
    /// <summary>
    /// Уникальный идентификатор автомобиля.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Бренд автомобиля (например, Toyota, BMW).
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Цвет кузова автомобиля.
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    /// Год выпуска автомобиля.
    /// </summary>
    public int Year { get; set; }

    /// <summary>
    /// Пробег автомобиля в километрах.
    /// </summary>
    public int Mileage { get; set; }

    /// <summary>
    /// Признак наличия тонировки окон. По умолчанию — false.
    /// </summary>
    public bool WindowTinting { get; set; } = false;

    /// <summary>
    /// Создаёт новый экземпляр автомобиля с заданными параметрами.
    /// </summary>
    /// <param name="id">Уникальный идентификатор автомобиля.</param>
    /// <param name="brand">Бренд автомобиля.</param>
    /// <param name="model">Модель автомобиля.</param>
    /// <param name="color">Цвет автомобиля.</param>
    /// <param name="year">Год выпуска автомобиля.</param>
    /// <param name="mileage">Пробег автомобиля в километрах.</param>
    public Car(int id, string brand, string model, string color, int year, int mileage)
    {
        Id = id;
        Brand = brand;
        Model = model;
        Color = color;
        Year = year;
        Mileage = mileage;
    }

    /// <summary>
    /// Возвращает строковое представление автомобиля,
    /// включающее все основные характеристики.
    /// </summary>
    /// <returns>Строка с описанием автомобиля.</returns>
    public override string ToString()
    {
        return "Id=" + Id + " | " + Brand + " " + Model + " | Цвет: " + Color + " | " + Year + " год | Пробег: " + Mileage + " км | Тонировка: " + WindowTinting;
    }
}
using System;

namespace Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }

        public Car(int id, string brand, string model, string color, int year)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Color = color;
            Year = year;
        }
        public override string ToString()
        {
            return "Id=" + Id + " | " + Brand + " " + Model + " | Цвет: " + Color + " | " + Year + " год";
        }
    }
}
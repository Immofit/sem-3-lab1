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
        public int Mileage { get; set; }
        public bool WindowTinting { get; set; }

        public Car(int id, string brand, string model, string color, int year, int mileage, bool windowTinting  )
        {
            Id = id;
            Brand = brand;
            Model = model;
            Color = color;
            Year = year;
            Mileage = mileage;
            WindowTinting = windowTinting;
        }
        public override string ToString()
        {
            return "Id=" + Id + " | " + Brand + " " + Model + " | Цвет: " + Color + " | " + Year + " год | Пробег: " + Mileage + " км | Остекление: " + WindowTinting;
        }
    }
}
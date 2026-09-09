using System;

namespace Model
{
    public class Car
    {
        public int Id;
        public string Brand;   
        public string Model;   
        public string Color;   
        public int Year;     
        public int Mileage;
        public bool WindowTinting;

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
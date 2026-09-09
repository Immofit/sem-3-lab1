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
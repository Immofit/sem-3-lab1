using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Linq;

namespace Model
{
    public class Logic
    {
        private List<Car> cars;
        private int nextId;
        public Logic()
        {
            cars = new List<Car>();
            nextId = 1;
        }
        //Создание машины
        public Car CreateCar(string brand, string model, string color, int year, int mileage)
        {
            var car = new Car(nextId++, brand, model, color, year, mileage   );
            cars.Add(car);
            return car;
        }
        //Удаление машины
        public bool DeleteCar(int id)
        {
            var car = cars.Find(c => c.Id == id);
            if (car != null)
            {
                cars.Remove(car);
                return true;
            }
            return false;
        }
        //Все машины
        public List<Car> AllCars()
        {
            return cars;
        }
        //По айди
        public Car CarId(int id)
        {
            return cars.Find(c => c.Id == id);
        }
        //Изменение машины
        public bool UpdateCar(int id, string brand, string model, string color, int year)
        {
            var car = cars.Find(c => c.Id == id);
            if (car != null)
            {
                car.Brand = brand;
                car.Model = model;
                car.Color = color;
                car.Year = year;
                return true;
            }
            return false;
        }
        //Групировка машин по бренду
        public Dictionary<string, List<Car>> CarsBrand()
        {
            var carsByBrand = new Dictionary<string, List<Car>>();
            foreach (var car in cars)
            {
                if (!carsByBrand.ContainsKey(car.Brand))
                {
                    carsByBrand[car.Brand] = new List<Car>();
                }
                carsByBrand[car.Brand].Add(car);
            }
            return carsByBrand;
        }
        //Групировка машин по году
        public List<Car> CarsYear(int year)
        {
            List<Car> result = new List<Car>();

            for (int i = 0; i < cars.Count; i++)
            {
                if (cars[i].Year == year)
                {
                    result.Add(cars[i]);
                }
            }

            return result;
        }

    }
}

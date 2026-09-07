namespace Model
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ModelName { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public double Mileage { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return $"[{Id}]" +
                $" {Brand}" +
                $" {ModelName}," +
                $" {Year} г., " +
                $"{Color}," +
                $" пробег {Mileage} км," +
                $" цена {Price} руб.";
        }
    }
}
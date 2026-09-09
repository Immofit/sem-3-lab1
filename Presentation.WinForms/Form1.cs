using Model;

namespace Presentation.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            logic.CreateCar("Toyota", "Camry", "Белый", 2018, 1000, false);
            logic.CreateCar("Lada", "Vesta", "Красный", 2021, 1500, false);
            logic.CreateCar("BMW", "X5", "Чёрный", 2023, 3000, false);
            logic.CreateCar("Mercedes", "GLX", "Сурый", 2026, 4000, false);
            logic.CreateCar("Ferrari", "Spider", "Синий", 2018, 5000, false);
            logic.CreateCar("Haval", "Dargo X", "Чёрный", 2024, 2000, false);
        }

        Logic logic = new Logic();


        private void GetCars_Click(object sender, EventArgs e)
        {
            Table.DataSource = logic.AllCars();
        }

        private void CreateCar_Click(object sender, EventArgs e)
        {

        }


        private void DeleteCar_Click(object sender, EventArgs e)
        {

        }
    }
}

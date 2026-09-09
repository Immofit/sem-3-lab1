using Model;
using System.ComponentModel;

namespace Presentation.WinForms
{
    public partial class Form1 : Form
    {

        private BindingList<Car> carsBinding;
        public Form1()
        {
            InitializeComponent();
            logic.CreateCar("Toyota", "Camry", "Белый", 2018);
            logic.CreateCar("Lada", "Vesta", "Красный", 2021);
            logic.CreateCar("BMW", "X5", "Чёрный", 2023);
            logic.CreateCar("Mercedes", "GLX", "Сурый", 2026);
            logic.CreateCar("Ferrari", "Spider", "Синий", 2018);
            logic.CreateCar("Haval", "Dargo X", "Чёрный", 2024);
        }

        Logic logic = new Logic();


        private void RefreshTable()
        {
            carsBinding = new BindingList<Car>(logic.AllCars());
            Table.DataSource = carsBinding;
        }




        private void GetCars_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void CreateCar_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddCarForm())
            {
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    var values = addForm.GetValues();
                    logic.CreateCar(values.brand, values.model, values.color, values.year);
                    RefreshTable();
                }
            }
        }


        private void DeleteCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not Car selectedCar)
            {
                MessageBox.Show("Сначала выберите машину в таблице");
                return;
            }

            logic.DeleteCar(selectedCar.Id);

            Table.ClearSelection();
            Table.DataSource = null;
            Table.DataSource = logic.AllCars();
        }
    }
}

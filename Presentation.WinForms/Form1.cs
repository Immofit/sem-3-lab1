using Microsoft.VisualBasic;
using Model;
using System.ComponentModel;

namespace Presentation.WinForms
{
    public partial class Form1 : Form
    {

        Logic logic = new Logic();
        private BindingList<Car> carsBinding;
        private bool secretMode = false;
        public Form1()
        {
            InitializeComponent();

            Table.ColumnHeaderMouseClick += Table_ColumnHeaderMouseClick;

            logic.CreateCar("Toyota", "Camry", "Белый", 2018);
            logic.CreateCar("Lada", "Vesta", "Красный", 2021);
            logic.CreateCar("BMW", "X5", "Чёрный", 2023);
            logic.CreateCar("Mercedes", "GLX", "Сурый", 2026);
            logic.CreateCar("Ferrari", "Spider", "Синий", 2018);
            logic.CreateCar("Haval", "Dargo X", "Чёрный", 2024);
        }


        private void Table_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var sortedCars = logic.AllCars().OrderBy(car => car.Brand).ToList();

                carsBinding = new BindingList<Car>(sortedCars);
                Table.DataSource = carsBinding;
            }
        }



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
                addForm.Text = "Добавить машину";
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

            Table.DataSource = logic.AllCars();
        }

        private void UpdateCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not Car selectedCar)
            {
                MessageBox.Show("Сначала выберите машину в таблице");
                return;
            }

            using (var addForm = new AddCarForm())
            {
                addForm.Text = "Изменить машину";
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    var values = addForm.GetValues();
                    logic.UpdateCar(selectedCar.Id, values.brand, values.model, values.color, values.year);
                    RefreshTable();
                }
            }
        }

        private void CarAge_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите год:", "Поиск машин", "");

            if (!int.TryParse(input, out int year))
            {
                MessageBox.Show("Введите корректный год.");
                return;
            }

            List<Car> result = logic.CarsYear(year);

            if (result.Count == 0)
            {
                MessageBox.Show("Машин " + year + " года не найдено.");
                return;
            }

            carsBinding = new BindingList<Car>(result);
            Table.DataSource = carsBinding;
        }

        private void ColorCar_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите цвет:", "Поиск машин", "");


            List<Car> result = logic.CarsColor(input);

            if (result.Count == 0)
            {
                MessageBox.Show("Машин цвета " + input + " не найдено.");
                return;
            }

            carsBinding = new BindingList<Car>(result);
            Table.DataSource = carsBinding;
        }

        private void Secret_Click(object sender, EventArgs e)
        {
            secretMode = !secretMode;


            if (secretMode)
            {
                pictureBox.Visible = true;
                Secret.Text = "Выйти из секретного режима";
                MileageDown.Visible = true;
                TiningSet.Visible = true;
            }
            else
            {
                // Выключаем секретный режим
                this.BackColor = SystemColors.Control;

                pictureBox.Visible = false;
                Secret.Text = "Секретные функции";
                MileageDown.Visible = false;
                TiningSet.Visible = false;
            }
        }
    }
}

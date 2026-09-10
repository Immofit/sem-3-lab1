using Microsoft.VisualBasic;
using Model;
using System.ComponentModel;
using System.Drawing;

namespace Presentation.WinForms
{
    public partial class Form1 : Form
    {

        Logic logic = new Logic();
        private BindingList<Car> carsBinding = new BindingList<Car>();
        private bool secretMode = false;



        /// <summary>
        /// Инициализирует форму и заполняет её тестовыми данными о машинах.
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            Table.ColumnHeaderMouseClick += Table_ColumnHeaderMouseClick;
        }

        /// <summary>
        /// Сортирует список машин по бренду при клике на заголовок соответствующего столбца.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные о столбце, по которому кликнули.</param>
        private void Table_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var sortedCars = logic.AllCars().OrderBy(car => car.Brand).ToList();

                carsBinding = new BindingList<Car>(sortedCars);
                Table.DataSource = carsBinding;
            }
        }


        /// <summary>
        /// Обновляет таблицу, заново получая полный список машин из Logic.
        /// </summary>
        private void RefreshTable()
        {
            carsBinding = new BindingList<Car>(logic.AllCars());
            Table.DataSource = carsBinding;
        }



        /// <summary>
        /// Обрабатывает нажатие кнопки "Получить все машины" — обновляет таблицу.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void GetCars_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }



        /// <summary>
        /// Обрабатывает нажатие кнопки "Создать машину" — открывает форму ввода данных
        /// и, если пользователь подтвердил ввод, создаёт новую машину через Logic.
        /// При некорректных данных (пустые поля, неверный год) показывает пользователю
        /// сообщение об ошибке вместо создания машины.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void CreateCar_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddCarForm())
            {
                addForm.Text = "Добавить машину";
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    var values = addForm.GetValues();

                    if (values.year < 1900 || values.year > DateTime.Now.Year)
                    {
                        MessageBox.Show("Год должен быть не меньше 1900 и не больше текущего.", "Ошибка ввода");
                        return;
                    }

                    try
                    {
                        logic.CreateCar(values.brand, values.model, values.color, values.year);
                        RefreshTable();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка ввода");
                    }
                }
            }
        }




        /// <summary>
        /// Обрабатывает нажатие кнопки "Удалить машину" — удаляет выбранную в таблице машину.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void DeleteCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not Car selectedCar)
            {
                MessageBox.Show("Сначала выберите машину в таблице");
                return;
            }

            Table.ClearSelection();
            Table.CurrentCell = null;

            logic.DeleteCar(selectedCar.Id);
            RefreshTable();
        }




        /// <summary>
        /// Обрабатывает нажатие кнопки "Изменить машину" — открывает форму редактирования
        /// для выбранной в таблице машины и применяет изменения через Logic.
        /// При некорректных данных (пустые поля, неверный год) показывает пользователю
        /// сообщение об ошибке вместо изменения машины.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
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

                    if (values.year < 1900 || values.year > DateTime.Now.Year)
                    {
                        MessageBox.Show("Год должен быть не меньше 1900 и не больше текущего.", "Ошибка ввода");
                        return;
                    }

                    try
                    {
                        logic.UpdateCar(selectedCar.Id, values.brand, values.model, values.color, values.year);
                        RefreshTable();
                    }
                    catch (ArgumentException ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка ввода");
                    }
                }
            }
        }




        /// <summary>
        /// Обрабатывает нажатие кнопки поиска машин по году — запрашивает год у пользователя
        /// и отображает только машины этого года выпуска.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void CarAge_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите год:", "Поиск машин", "");

            if (!int.TryParse(input, out int year) || year < 1900 || year > DateTime.Now.Year)
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



        /// <summary>
        /// Обрабатывает нажатие кнопки поиска машин по цвету — запрашивает цвет у пользователя
        /// и отображает только машины этого цвета.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void ColorCar_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите цвет:", "Поиск машин", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("Цвет не может быть пустым.");
                return;
            }

            List<Car> result = logic.CarsColor(input);
            if (result.Count == 0)
            {
                MessageBox.Show("Машин цвета " + input + " не найдено.");
                return;
            }

            carsBinding = new BindingList<Car>(result);
            Table.DataSource = carsBinding;
        }





        /// <summary>
        /// Переключает видимость секретных функций (тонировка, скрутка пробега)
        /// и меняет визуальное оформление формы в зависимости от режима.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void Secret_Click(object sender, EventArgs e)
        {
            secretMode = !secretMode;


            if (secretMode)
            {
                this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);

                pictureBox.Visible = true;
                Secret.Text = "Выйти из секретного режима";
                MileageDown.Visible = true;
                TiningSet.Visible = true;
            }
            else
            {
                this.BackColor = SystemColors.Control;

                pictureBox.Visible = false;
                Secret.Text = "Секретные функции";
                MileageDown.Visible = false;
                TiningSet.Visible = false;
            }
        }



        /// <summary>
        /// Обрабатывает нажатие кнопки "Скрутить пробег" — запрашивает у пользователя
        /// новое значение пробега и применяет его к выбранной в таблице машине через Logic.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void RollBackMileage_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not Car selectedCar)
            {
                MessageBox.Show("Сначала выберите машину в таблице");
                return;
            }

            string input = Interaction.InputBox("Введите пробег:", "Скрутить пробег", "");

            if (!int.TryParse(input, out int mileage) || mileage < 0)
            {
                MessageBox.Show("Введите корректный пробег (неотрицательное число).");
                return;
            }

            bool success = logic.RollBackMileage(selectedCar.Id, mileage);
            if (!success)
            {
                MessageBox.Show("Новый пробег должен быть меньше текущего.");
                return;
            }

            RefreshTable();
        }


        /// <summary>
        /// Обрабатывает нажатие кнопки "Поставить тонировку" — устанавливает тонировку
        /// окон для выбранной в таблице машины через Logic.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void AddTinting_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not Car selectedCar)
            {
                MessageBox.Show("Сначала выберите машину в таблице");
                return;
            }

            bool success = logic.AddTinting(selectedCar.Id);
            if (!success)
            {
                MessageBox.Show("Тонировка уже установлена на эту машину.");
                return;
            }

            RefreshTable();
        }
    }
}

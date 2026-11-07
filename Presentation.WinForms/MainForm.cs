using Microsoft.VisualBasic;
using System.ComponentModel;
using Shared;

namespace Presentation.WinForms
{
    public partial class MainForm : Form, IView
    {
        private BindingList<CarDto> carsBinding = new BindingList<CarDto>();
        private bool secretMode = false;

        public event Action? RefreshRequested;
        public event Action<CarDto>? CreateRequested;
        public event Action<CarDto>? UpdateRequested;
        public event Action<int>? DeleteRequested;
        public event Action? BrandGroupingRequested;
        public event Action<int>? FindByYearRequested;
        public event Action<string>? FindByColorRequested;
        public event Action<int>? AddTintingRequested;
        public event Action<int, int>? RollBackMileageRequested;

        public MainForm()
        {
            InitializeComponent();
            Table.ColumnHeaderMouseClick += Table_ColumnHeaderMouseClick;
        }

        // ---------- Методы IView: отрисовка ----------

        /// <summary>Показывает список машин в таблице.</summary>
        public void ShowCars(List<CarDto> cars)
        {
            carsBinding = new BindingList<CarDto>(cars);
            Table.DataSource = carsBinding;
        }

        /// <summary>Показывает машины, отсортированные по бренду (группы идут подряд).</summary>
        public void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups)
        {
            ShowCars(groups.OrderBy(group => group.Key).SelectMany(group => group.Value).ToList());
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка");
        }

        // ---------- События интерфейса: только передаём запрос Presenter ----------

        private void Table_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                BrandGroupingRequested?.Invoke();
            }
        }

        private void GetCars_Click(object sender, EventArgs e)
        {
            RefreshRequested?.Invoke();
        }

        private void CreateCar_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddCarForm())
            {
                addForm.Text = "Добавить машину";
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    var values = addForm.GetValues();
                    CreateRequested?.Invoke(new CarDto
                    {
                        Brand = values.brand,
                        Model = values.model,
                        Color = values.color,
                        Year = values.year
                    });
                }
            }
        }

        private void DeleteCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            Table.ClearSelection();
            Table.CurrentCell = null;

            DeleteRequested?.Invoke(selectedCar.Id);
        }

        private void UpdateCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            using (var addForm = new AddCarForm())
            {
                addForm.Text = "Изменить машину";
                if (addForm.ShowDialog(this) == DialogResult.OK)
                {
                    var values = addForm.GetValues();
                    UpdateRequested?.Invoke(new CarDto
                    {
                        Id = selectedCar.Id,
                        Brand = values.brand,
                        Model = values.model,
                        Color = values.color,
                        Year = values.year
                    });
                }
            }
        }

        private void CarAge_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите год:", "Поиск машин", "");

            if (!int.TryParse(input, out int year))
            {
                ShowError("Введите корректный год.");
                return;
            }

            FindByYearRequested?.Invoke(year);
        }

        private void ColorCar_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите цвет:", "Поиск машин", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                ShowError("Цвет не может быть пустым.");
                return;
            }

            FindByColorRequested?.Invoke(input);
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

        private void RollBackMileage_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            string input = Interaction.InputBox("Введите пробег:", "Скрутить пробег", "");

            if (!int.TryParse(input, out int mileage))
            {
                ShowError("Введите корректное число.");
                return;
            }

            RollBackMileageRequested?.Invoke(selectedCar.Id, mileage);
        }

        private void AddTinting_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            AddTintingRequested?.Invoke(selectedCar.Id);
        }
    }
}
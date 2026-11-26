using Microsoft.VisualBasic;
using System.ComponentModel;
using Shared;

namespace Presentation.WinForms
{
    public partial class MainForm : Form, IView
    {
        /// <summary>
        /// Список машин, привязанный к таблице.
        /// </summary>
        private BindingList<CarDto> carsBinding = new BindingList<CarDto>();

        /// <summary>
        /// true, если включён секретный режим (видны кнопки тонировки и скрутки пробега).
        /// </summary>
        private bool secretMode = false;

        /// <summary>
        /// Возникает, когда пользователь просит показать все машины.
        /// </summary>
        public event Action? RefreshRequested;

        /// <summary>
        /// Возникает, когда пользователь подтвердил создание машины.
        /// Передаёт введённые бренд, модель, цвет и год.
        /// </summary>
        public event Action<CarDto>? CreateRequested;

        /// <summary>
        /// Возникает, когда пользователь подтвердил изменение машины.
        /// Передаёт Id и новые бренд, модель, цвет и год.
        /// </summary>
        public event Action<CarDto>? UpdateRequested;

        /// <summary>
        /// Возникает, когда пользователь просит удалить машину.
        /// Передаёт Id выбранной машины.
        /// </summary>
        public event Action<int>? DeleteRequested;

        /// <summary>
        /// Возникает, когда пользователь просит сгруппировать машины по бренду.
        /// </summary>
        public event Action? BrandGroupingRequested;

        /// <summary>
        /// Возникает, когда пользователь просит найти машины по году.
        /// Передаёт введённый год.
        /// </summary>
        public event Action<int>? FindByYearRequested;

        /// <summary>
        /// Возникает, когда пользователь просит найти машины по цвету.
        /// Передаёт введённый цвет.
        /// </summary>
        public event Action<string>? FindByColorRequested;

        /// <summary>
        /// Возникает, когда пользователь просит добавить тонировку. 
        /// Передаёт Id выбранной машины.
        /// </summary>
        public event Action<int>? AddTintingRequested;

        /// <summary>
        /// Возникает, когда пользователь просит скрутить пробег.
        /// Передаёт Id выбранной машины и новый пробег.
        /// </summary>
        public event Action<int, int>? RollBackMileageRequested;

        /// <summary>
        /// Создаёт главную форму: строит элементы из дизайнера и подписывает
        /// таблицу на щелчок по заголовку столбца.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Table.ColumnHeaderMouseClick += Table_ColumnHeaderMouseClick;
        }



        /// <summary>
        /// Показывает список машин в таблице: оборачивает его
        /// в BindingList и назначает источником данных таблицы.
        /// </summary>
        /// <param name="cars">Список машин для отображения.</param>
        public void ShowCars(List<CarDto> cars)
        {
            carsBinding = new BindingList<CarDto>(cars);
            Table.DataSource = carsBinding;
        }

        /// <summary>
        /// Показывает машины в таблице, отсортированные по бренду:
        /// машины одного бренда идут подряд.
        /// </summary>
        /// <param name="groups">Словарь, где ключ — бренд, значение — список машин этого бренда.</param>
        public void ShowCarsByBrand(Dictionary<string, List<CarDto>> groups)
        {
            List<string> brands = new List<string>(groups.Keys);
            brands.Sort();

            List<CarDto> sorted = new List<CarDto>();
            foreach (string brand in brands)
            {
                sorted.AddRange(groups[brand]);
            }

            ShowCars(sorted);
        }

        /// <summary>
        /// Показывает окно с информационным сообщением.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Показывает окно с сообщением об ошибке и заголовком «Ошибка».
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка");
        }



        /// <summary>
        /// Обрабатывает щелчок по заголовку столбца таблицы: если это столбец с брендом (индекс 1),
        /// вызывает событие группировки по бренду.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события, в том числе индекс столбца.</param>
        private void Table_ColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (BrandGroupingRequested != null)
                {
                    BrandGroupingRequested();
                }
            }
        }

        /// <summary>
        /// Обрабатывает нажатие кнопки показа машин: вызывает событие обновления списка.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void GetCars_Click(object sender, EventArgs e)
        {
            if (RefreshRequested != null)
            {
                RefreshRequested();
            }
        }

        /// <summary>
        /// Открывает форму ввода данных новой машины; если пользователь подтвердил ввод,
        /// вызывает событие создания с введёнными значениями.
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

                    CarDto car = new CarDto();
                    car.Brand = values.brand;
                    car.Model = values.model;
                    car.Color = values.color;
                    car.Year = values.year;

                    if (CreateRequested != null)
                    {
                        CreateRequested(car);
                    }
                }
            }
        }

        /// <summary>
        /// Берёт машину, выбранную в таблице; если машина не выбрана, показывает ошибку,
        /// иначе снимает выделение и вызывает событие удаления с её Id.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void DeleteCar_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            Table.ClearSelection();
            Table.CurrentCell = null;

            if (DeleteRequested != null)
            {
                DeleteRequested(selectedCar.Id);
            }
        }

        /// <summary>
        /// Берёт машину, выбранную в таблице, открывает форму с полями для новых данных;
        /// если пользователь подтвердил ввод,
        /// вызывает событие изменения с Id машины и введёнными значениями.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
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

                    CarDto car = new CarDto();
                    car.Id = selectedCar.Id;
                    car.Brand = values.brand;
                    car.Model = values.model;
                    car.Color = values.color;
                    car.Year = values.year;

                    if (UpdateRequested != null)
                    {
                        UpdateRequested(car);
                    }
                }
            }
        }

        /// <summary>
        /// Запрашивает год в окне ввода, проверяет, что введено число,
        /// и вызывает событие поиска по году.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void CarAge_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите год:", "Поиск машин", "");

            if (!int.TryParse(input, out int year))
            {
                ShowError("Введите корректный год.");
                return;
            }

            if (FindByYearRequested != null)
            {
                FindByYearRequested(year);
            }
        }

        /// <summary>
        /// Запрашивает цвет в окне ввода, проверяет, что строка не пустая,
        /// и вызывает событие поиска по цвету.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void ColorCar_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Введите цвет:", "Поиск машин", "");

            if (string.IsNullOrWhiteSpace(input))
            {
                ShowError("Цвет не может быть пустым.");
                return;
            }

            if (FindByColorRequested != null)
            {
                FindByColorRequested(input);
            }
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
        /// Берёт машину, выбранную в таблице,
        /// запрашивает новый пробег в окне ввода, проверяет, что введено число,
        /// и вызывает событие скрутки пробега.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
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

            if (RollBackMileageRequested != null)
            {
                RollBackMileageRequested(selectedCar.Id, mileage);
            }
        }

        /// <summary>
        /// Берёт машину, выбранную в таблице, и вызывает событие добавления тонировки с её Id.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void AddTinting_Click(object sender, EventArgs e)
        {
            if (Table.CurrentRow?.DataBoundItem is not CarDto selectedCar)
            {
                ShowError("Сначала выберите машину в таблице");
                return;
            }

            if (AddTintingRequested != null)
            {
                AddTintingRequested(selectedCar.Id);
            }
        }
    }
}
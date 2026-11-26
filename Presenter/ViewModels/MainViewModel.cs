using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using BusinessLogic;
using Shared;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Главная ViewModel приложения.
    /// Работает с бизнес-логикой через ILogic, хранит список CarDto в BindingList
    /// и синхронизирует данные с моделью при вызове бизнес-функций.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private ILogic logic;
        private ViewModelManager viewModelManager;

        private CarDto? selectedCar;
        private bool isSecretMode;

        /// <summary>
        /// Список машин (DTO) для отображения в таблице.
        /// Используется BindingList&lt;CarDto&gt; для поддержки уведомлений об изменениях.
        /// </summary>
        public BindingList<CarDto> Cars { get; }

        /// <summary>
        /// Выбранная пользователем машина в таблице.
        /// </summary>
        public CarDto? SelectedCar
        {
            get { return selectedCar; }
            set
            {
                if (selectedCar != value)
                {
                    selectedCar = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// true, если включён секретный режим (показываются кнопки тонировки и скрутки пробега и меняется фон).
        /// </summary>
        public bool IsSecretMode
        {
            get { return isSecretMode; }
            set
            {
                if (isSecretMode != value)
                {
                    isSecretMode = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(SecretButtonText));
                }
            }
        }

        /// <summary>
        /// Текст кнопки переключения секретного режима.
        /// </summary>
        public string SecretButtonText
        {
            get
            {
                if (isSecretMode)
                {
                    return "Выйти из секретного режима";
                }
                return "Секретные функции";
            }
        }

        /// <summary>Команда обновления списка всех машин.</summary>
        public ICommand RefreshCommand { get; }

        /// <summary>Команда создания новой машины.</summary>
        public ICommand CreateCarCommand { get; }

        /// <summary>Команда изменения выбранной машины.</summary>
        public ICommand UpdateCarCommand { get; }

        /// <summary>Команда удаления выбранной машины.</summary>
        public ICommand DeleteCarCommand { get; }

        /// <summary>Команда группировки машин по бренду.</summary>
        public ICommand GroupByBrandCommand { get; }

        /// <summary>Команда поиска машин по году выпуска.</summary>
        public ICommand FindByYearCommand { get; }

        /// <summary>Команда поиска машин по цвету.</summary>
        public ICommand FindByColorCommand { get; }

        /// <summary>Команда переключения секретного режима.</summary>
        public ICommand ToggleSecretModeCommand { get; }

        /// <summary>Команда добавления тонировки выбранной машине.</summary>
        public ICommand AddTintingCommand { get; }

        /// <summary>Команда скручивания пробега выбранной машины.</summary>
        public ICommand RollBackMileageCommand { get; }

        /// <summary>
        /// Инициализирует главную ViewModel, создаёт команды, подписывается на изменения модели
        /// и загружает начальный список машин.
        /// </summary>
        /// <param name="logic">Бизнес-логика для работы с машинами.</param>
        /// <param name="viewModelManager">Менеджер ViewModel для открытия окон и показа сообщений.</param>
        public MainViewModel(ILogic logic, ViewModelManager viewModelManager)
        {
            this.logic = logic;
            this.viewModelManager = viewModelManager;

            Cars = new BindingList<CarDto>();

            RefreshCommand = new RelayCommand(RefreshCars);
            CreateCarCommand = new RelayCommand(CreateCar);
            UpdateCarCommand = new RelayCommand(UpdateCar);
            DeleteCarCommand = new RelayCommand(DeleteCar);
            GroupByBrandCommand = new RelayCommand(GroupByBrand);
            FindByYearCommand = new RelayCommand(FindByYear);
            FindByColorCommand = new RelayCommand(FindByColor);
            ToggleSecretModeCommand = new RelayCommand(ToggleSecretMode);
            AddTintingCommand = new RelayCommand(AddTinting);
            RollBackMileageCommand = new RelayCommand(RollBackMileage);

            this.logic.CarsChanged += OnCarsChanged;
            SyncWithModel();
        }

        /// <summary>
        /// Обрабатывает событие изменения данных в бизнес-логике и синхронизирует список DTO.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Данные события.</param>
        private void OnCarsChanged(object? sender, EventArgs e)
        {
            SyncWithModel();
        }

        /// <summary>
        /// Синхронизирует BindingList&lt;CarDto&gt; с актуальными данными из бизнес-логики.
        /// Обновляет свойства существующих DTO (вызывая INotifyPropertyChanged),
        /// добавляет новые машины и удаляет отсутствующие.
        /// </summary>
        private void SyncWithModel()
        {
            List<CarDto> freshCars = logic.AllCars();

            // Удаляем из списка машины, которых больше нет в модели
            for (int i = Cars.Count - 1; i >= 0; i--)
            {
                if (!ContainsCarId(freshCars, Cars[i].Id))
                {
                    if (SelectedCar != null && SelectedCar.Id == Cars[i].Id)
                    {
                        SelectedCar = null;
                    }
                    Cars.RemoveAt(i);
                }
            }

            // Обновляем существующие DTO и добавляем новые
            foreach (CarDto fresh in freshCars)
            {
                CarDto? existing = FindCarById(fresh.Id);
                if (existing != null)
                {
                    existing.Brand = fresh.Brand;
                    existing.Model = fresh.Model;
                    existing.Color = fresh.Color;
                    existing.Year = fresh.Year;
                    existing.Mileage = fresh.Mileage;
                    existing.WindowTinting = fresh.WindowTinting;
                }
                else
                {
                    Cars.Add(fresh);
                }
            }
        }

        /// <summary>
        /// Заменяет текущий отображаемый список машин на переданный (для поиска и группировки).
        /// </summary>
        /// <param name="cars">Список машин для отображения.</param>
        private void ReplaceCars(List<CarDto> cars)
        {
            SelectedCar = null;
            Cars.Clear();
            foreach (CarDto car in cars)
            {
                Cars.Add(car);
            }
        }

        /// <summary>
        /// Ищет объект CarDto в текущем BindingList по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>Найденный CarDto или null.</returns>
        private CarDto? FindCarById(int id)
        {
            foreach (CarDto car in Cars)
            {
                if (car.Id == id)
                {
                    return car;
                }
            }
            return null;
        }

        /// <summary>
        /// Проверяет, есть ли в списке машина с указанным идентификатором.
        /// </summary>
        /// <param name="cars">Список машин для проверки.</param>
        /// <param name="id">Идентификатор машины.</param>
        /// <returns>true, если машина найдена; иначе false.</returns>
        private bool ContainsCarId(List<CarDto> cars, int id)
        {
            foreach (CarDto car in cars)
            {
                if (car.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Показывает все машины из бизнес-логики (сбрасывает фильтры и группировку).
        /// </summary>
        private void RefreshCars()
        {
            ReplaceCars(logic.AllCars());
        }

        /// <summary>
        /// Открывает окно создания машины и сохраняет её через бизнес-логику.
        /// </summary>
        private void CreateCar()
        {
            CarDto draft = new CarDto();
            draft.Year = DateTime.Now.Year;

            CarDto? entered = viewModelManager.ShowCarEditor("Добавить машину", draft);
            if (entered == null)
            {
                return;
            }

            CarDto? created = logic.CreateCar(entered.Brand, entered.Model, entered.Color, entered.Year);
            if (created == null)
            {
                viewModelManager.ShowError("Машина не создана: данные некорректны (проверьте поля и год от 1900 до текущего).");
                return;
            }

            viewModelManager.ShowMessage("Машина создана.");
        }

        /// <summary>
        /// Открывает окно изменения выбранной машины и сохраняет новые данные через бизнес-логику.
        /// </summary>
        private void UpdateCar()
        {
            if (SelectedCar == null)
            {
                viewModelManager.ShowError("Сначала выберите машину в таблице.");
                return;
            }

            // Передаём копию DTO, чтобы при отмене редактирования не испортить данные в таблице
            CarDto copy = new CarDto();
            copy.Id = SelectedCar.Id;
            copy.Brand = SelectedCar.Brand;
            copy.Model = SelectedCar.Model;
            copy.Color = SelectedCar.Color;
            copy.Year = SelectedCar.Year;
            copy.Mileage = SelectedCar.Mileage;
            copy.WindowTinting = SelectedCar.WindowTinting;

            CarDto? edited = viewModelManager.ShowCarEditor("Изменить машину", copy);
            if (edited == null)
            {
                return;
            }

            if (logic.UpdateCar(edited.Id, edited.Brand, edited.Model, edited.Color, edited.Year))
            {
                viewModelManager.ShowMessage("Данные машины изменены.");
            }
            else
            {
                viewModelManager.ShowError("Данные машины не изменены: машина не найдена или данные некорректны.");
            }
        }

        /// <summary>
        /// Удаляет выбранную машину через бизнес-логику.
        /// </summary>
        private void DeleteCar()
        {
            if (SelectedCar == null)
            {
                viewModelManager.ShowError("Сначала выберите машину в таблице.");
                return;
            }

            int id = SelectedCar.Id;
            SelectedCar = null;

            if (logic.DeleteCar(id))
            {
                viewModelManager.ShowMessage("Машина удалена.");
            }
            else
            {
                viewModelManager.ShowError("Машина не удалена: не найдена.");
            }
        }

        /// <summary>
        /// Группирует машины по бренду и выводит их группами в алфавитном порядке брендов.
        /// </summary>
        private void GroupByBrand()
        {
            Dictionary<string, List<CarDto>> groups = logic.CarsBrand();

            List<string> brands = new List<string>(groups.Keys);
            brands.Sort();

            List<CarDto> sorted = new List<CarDto>();
            foreach (string brand in brands)
            {
                sorted.AddRange(groups[brand]);
            }

            ReplaceCars(sorted);
        }

        /// <summary>
        /// Запрашивает год выпуска и показывает машины этого года.
        /// </summary>
        private void FindByYear()
        {
            string? input = viewModelManager.ShowInput("Поиск машин по году", "Введите год выпуска:");
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out int year))
            {
                viewModelManager.ShowError("Введите корректный год.");
                return;
            }

            List<CarDto> found = logic.CarsYear(year);
            if (found.Count == 0)
            {
                viewModelManager.ShowMessage("Машин указанного года не найдено.");
                return;
            }

            ReplaceCars(found);
        }

        /// <summary>
        /// Запрашивает цвет и показывает машины этого цвета.
        /// </summary>
        private void FindByColor()
        {
            string? input = viewModelManager.ShowInput("Поиск машин по цвету", "Введите цвет:");
            if (input == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                viewModelManager.ShowError("Цвет не может быть пустым.");
                return;
            }

            List<CarDto> found = logic.CarsColor(input.Trim());
            if (found.Count == 0)
            {
                viewModelManager.ShowMessage("Машин указанного цвета не найдено.");
                return;
            }

            ReplaceCars(found);
        }

        /// <summary>
        /// Переключает секретный режим интерфейса.
        /// </summary>
        private void ToggleSecretMode()
        {
            IsSecretMode = !IsSecretMode;
        }

        /// <summary>
        /// Добавляет тонировку выбранной машине через бизнес-логику.
        /// </summary>
        private void AddTinting()
        {
            if (SelectedCar == null)
            {
                viewModelManager.ShowError("Сначала выберите машину в таблице.");
                return;
            }

            if (logic.AddTinting(SelectedCar.Id))
            {
                viewModelManager.ShowMessage("Тонировка добавлена.");
            }
            else
            {
                viewModelManager.ShowError("Тонировка не добавлена: машина не найдена или тонировка уже стоит.");
            }
        }

        /// <summary>
        /// Запрашивает новый пробег и скручивает пробег выбранной машины через бизнес-логику.
        /// </summary>
        private void RollBackMileage()
        {
            if (SelectedCar == null)
            {
                viewModelManager.ShowError("Сначала выберите машину в таблице.");
                return;
            }

            string? input = viewModelManager.ShowInput("Скрутить пробег", "Введите новый пробег (меньше текущего):");
            if (input == null)
            {
                return;
            }

            if (!int.TryParse(input, out int newMileage))
            {
                viewModelManager.ShowError("Введите корректное число.");
                return;
            }

            if (logic.RollBackMileage(SelectedCar.Id, newMileage))
            {
                viewModelManager.ShowMessage("Пробег скручен.");
            }
            else
            {
                viewModelManager.ShowError("Пробег не скручен: машина не найдена или значение не меньше текущего.");
            }
        }
    }
}

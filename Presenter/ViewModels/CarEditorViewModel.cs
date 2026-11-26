using System.Windows.Input;
using Shared;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для диалогового окна добавления и редактирования автомобиля.
    /// Работает с объектом CarDto и закрывает окно через ViewModelManager.
    /// </summary>
    public class CarEditorViewModel : BaseViewModel
    {
        private ViewModelManager viewModelManager;
        private string yearText;

        /// <summary>
        /// Заголовок окна редактора.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Редактируемый объект DTO автомобиля.
        /// </summary>
        public CarDto Car { get; }

        /// <summary>
        /// Текстовое представление года выпуска для удобного ввода и проверки корректности числа.
        /// </summary>
        public string YearText
        {
            get { return yearText; }
            set
            {
                if (yearText != value)
                {
                    yearText = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>Команда подтверждения ввода и закрытия окна.</summary>
        public ICommand ApplyCommand { get; }

        /// <summary>Команда отмены и закрытия окна без сохранения.</summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Инициализирует ViewModel редактора машины.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="car">DTO машины с начальными значениями.</param>
        /// <param name="viewModelManager">Менеджер ViewModel для вывода ошибок и закрытия окна.</param>
        public CarEditorViewModel(string title, CarDto car, ViewModelManager viewModelManager)
        {
            Title = title;
            Car = car;
            this.viewModelManager = viewModelManager;
            yearText = car.Year > 0 ? car.Year.ToString() : "";

            ApplyCommand = new RelayCommand(Apply);
            CancelCommand = new RelayCommand(Cancel);
        }

        /// <summary>
        /// Проверяет заполненность полей и корректность года, затем закрывает диалог с результатом true.
        /// </summary>
        private void Apply()
        {
            if (string.IsNullOrWhiteSpace(Car.Brand) ||
                string.IsNullOrWhiteSpace(Car.Model) ||
                string.IsNullOrWhiteSpace(Car.Color))
            {
                viewModelManager.ShowError("Заполните бренд, модель и цвет.");
                return;
            }

            if (!int.TryParse(YearText, out int parsedYear))
            {
                viewModelManager.ShowError("Введите корректный год выпуска.");
                return;
            }

            Car.Brand = Car.Brand.Trim();
            Car.Model = Car.Model.Trim();
            Car.Color = Car.Color.Trim();
            Car.Year = parsedYear;

            viewModelManager.CloseDialog(this, true);
        }

        /// <summary>
        /// Закрывает диалог с результатом false.
        /// </summary>
        private void Cancel()
        {
            viewModelManager.CloseDialog(this, false);
        }
    }
}

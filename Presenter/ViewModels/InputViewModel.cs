using System.Windows.Input;

namespace Presenter.ViewModels
{
    /// <summary>
    /// ViewModel для диалогового окна ввода одного текстового значения.
    /// Используется при поиске по году, поиску по цвету и вводе нового пробега.
    /// </summary>
    public class InputViewModel : BaseViewModel
    {
        private ViewModelManager viewModelManager;
        private string inputText = "";

        /// <summary>
        /// Заголовок диалогового окна.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Текст подсказки для пользователя.
        /// </summary>
        public string Prompt { get; }

        /// <summary>
        /// Введённое пользователем значение.
        /// </summary>
        public string InputText
        {
            get { return inputText; }
            set
            {
                if (inputText != value)
                {
                    inputText = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>Команда подтверждения ввода.</summary>
        public ICommand ConfirmCommand { get; }

        /// <summary>Команда отмены ввода.</summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Инициализирует ViewModel окна ввода значения.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="prompt">Подсказка над полем ввода.</param>
        /// <param name="viewModelManager">Менеджер ViewModel для закрытия окна.</param>
        public InputViewModel(string title, string prompt, ViewModelManager viewModelManager)
        {
            Title = title;
            Prompt = prompt;
            this.viewModelManager = viewModelManager;

            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);
        }

        /// <summary>
        /// Подтверждает ввод и закрывает окно с результатом true.
        /// </summary>
        private void Confirm()
        {
            viewModelManager.CloseDialog(this, true);
        }

        /// <summary>
        /// Отменяет ввод и закрывает окно с результатом false.
        /// </summary>
        private void Cancel()
        {
            viewModelManager.CloseDialog(this, false);
        }
    }
}

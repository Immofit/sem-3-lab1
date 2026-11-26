using BusinessLogic;
using Shared;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Менеджер ViewModel (реализация подхода ViewModelFirst).
    /// Сначала создаёт нужную ViewModel, а затем просит IViewManager создать и показать для неё окно.
    /// </summary>
    public class ViewModelManager
    {
        private ILogic logic;
        private IViewManager viewManager;

        /// <summary>
        /// Создаёт менеджер ViewModel с бизнес-логикой и менеджером представлений.
        /// </summary>
        /// <param name="logic">Бизнес-логика приложения.</param>
        /// <param name="viewManager">Менеджер представлений WPF.</param>
        public ViewModelManager(ILogic logic, IViewManager viewManager)
        {
            this.logic = logic;
            this.viewManager = viewManager;
        }

        /// <summary>
        /// Запускает главное окно: создаёт MainViewModel и открывает для неё главное представление.
        /// </summary>
        public void ShowMain()
        {
            MainViewModel mainViewModel = new MainViewModel(logic, this);
            viewManager.ShowMainView(mainViewModel);
        }

        /// <summary>
        /// Открывает окно добавления или изменения машины.
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="initialCar">Начальные данные машины для заполнения полей.</param>
        /// <returns>Заполненный объект CarDto, если пользователь нажал «Применить»; иначе null.</returns>
        public CarDto? ShowCarEditor(string title, CarDto initialCar)
        {
            CarEditorViewModel editorViewModel = new CarEditorViewModel(title, initialCar, this);
            bool confirmed = viewManager.ShowCarEditorView(editorViewModel);
            if (!confirmed)
            {
                return null;
            }
            return editorViewModel.Car;
        }

        /// <summary>
        /// Открывает окно для ввода одного текстового значения (года, цвета или пробега).
        /// </summary>
        /// <param name="title">Заголовок окна.</param>
        /// <param name="prompt">Подсказка над полем ввода.</param>
        /// <returns>Введённая строка, если пользователь нажал «ОК»; иначе null.</returns>
        public string? ShowInput(string title, string prompt)
        {
            InputViewModel inputViewModel = new InputViewModel(title, prompt, this);
            bool confirmed = viewManager.ShowInputView(inputViewModel);
            if (!confirmed)
            {
                return null;
            }
            return inputViewModel.InputText;
        }

        /// <summary>
        /// Закрывает обычное окно, связанное с указанной ViewModel.
        /// </summary>
        /// <param name="viewModel">ViewModel закрываемого окна.</param>
        public void CloseView(BaseViewModel viewModel)
        {
            viewManager.CloseView(viewModel, null);
        }

        /// <summary>
        /// Закрывает модальное диалоговое окно с указанным результатом.
        /// </summary>
        /// <param name="viewModel">ViewModel закрываемого диалога.</param>
        /// <param name="dialogResult">true — подтверждено, false — отменено.</param>
        public void CloseDialog(BaseViewModel viewModel, bool dialogResult)
        {
            viewManager.CloseView(viewModel, dialogResult);
        }

        /// <summary>
        /// Показывает пользователю информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void ShowMessage(string message)
        {
            viewManager.ShowMessage(message);
        }

        /// <summary>
        /// Показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public void ShowError(string message)
        {
            viewManager.ShowError(message);
        }
    }
}

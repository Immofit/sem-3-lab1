namespace Shared
{
    /// <summary>
    /// Интерфейс менеджера представлений для паттерна ViewModelFirst.
    /// Позволяет ViewModelManager открывать и закрывать окна WPF,
    /// не используя рефлексию и тип object.
    /// </summary>
    public interface IViewManager
    {
        /// <summary>
        /// Создаёт и открывает главное окно, привязывая к нему переданную ViewModel.
        /// </summary>
        /// <param name="viewModel">Главная ViewModel приложения.</param>
        void ShowMainView(IViewModel viewModel);

        /// <summary>
        /// Создаёт и открывает модальное окно добавления или редактирования машины.
        /// </summary>
        /// <param name="viewModel">ViewModel редактора машины.</param>
        /// <returns>true, если пользователь подтвердил ввод; иначе false.</returns>
        bool ShowCarEditorView(IViewModel viewModel);

        /// <summary>
        /// Создаёт и открывает модальное окно ввода одного текстового значения.
        /// </summary>
        /// <param name="viewModel">ViewModel окна ввода.</param>
        /// <returns>true, если пользователь подтвердил ввод; иначе false.</returns>
        bool ShowInputView(IViewModel viewModel);

        /// <summary>
        /// Закрывает окно, связанное с указанной ViewModel.
        /// </summary>
        /// <param name="viewModel">ViewModel закрываемого окна.</param>
        /// <param name="dialogResult">Результат диалога для модального окна или null для обычного.</param>
        void CloseView(IViewModel viewModel, bool? dialogResult = null);

        /// <summary>
        /// Показывает пользователю информационное сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Показывает пользователю сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        void ShowError(string message);
    }
}

using System.Collections.Generic;
using System.Windows;
using Shared;

namespace Presentation.Wpf
{
    /// <summary>
    /// Менеджер представлений WPF.
    /// Создаёт конкретные окна (наследники BaseView), привязывает к ним ViewModel,
    /// отслеживает открытые окна и управляет их закрытием без рефлексии и типа object.
    /// </summary>
    public class ViewManager : IViewManager
    {
        /// <summary>
        /// Словарь соответствия открытых ViewModel и связанных с ними окон.
        /// </summary>
        private Dictionary<IViewModel, BaseView> openViews = new Dictionary<IViewModel, BaseView>();

        /// <summary>
        /// Ссылка на открытое главное окно (используется как Owner для модальных диалогов).
        /// </summary>
        private BaseView? mainView;

        /// <summary>
        /// Создаёт и открывает главное окно, привязывая к нему переданную ViewModel.
        /// </summary>
        /// <param name="viewModel">Главная ViewModel приложения.</param>
        public void ShowMainView(IViewModel viewModel)
        {
            MainWindow view = new MainWindow();
            view.BindViewModel(viewModel);

            mainView = view;
            openViews[viewModel] = view;
            view.ViewClosed += () =>
            {
                openViews.Remove(viewModel);
                if (mainView == view)
                {
                    mainView = null;
                }
            };

            view.ShowView();
        }

        /// <summary>
        /// Создаёт и открывает модальное окно добавления или редактирования машины.
        /// </summary>
        /// <param name="viewModel">ViewModel редактора машины.</param>
        /// <returns>true, если пользователь подтвердил ввод; иначе false.</returns>
        public bool ShowCarEditorView(IViewModel viewModel)
        {
            CarEditorWindow view = new CarEditorWindow();
            return ShowModalView(view, viewModel);
        }

        /// <summary>
        /// Создаёт и открывает модальное окно ввода одного текстового значения.
        /// </summary>
        /// <param name="viewModel">ViewModel окна ввода.</param>
        /// <returns>true, если пользователь подтвердил ввод; иначе false.</returns>
        public bool ShowInputView(IViewModel viewModel)
        {
            InputWindow view = new InputWindow();
            return ShowModalView(view, viewModel);
        }

        /// <summary>
        /// Закрывает окно, связанное с указанной ViewModel.
        /// </summary>
        /// <param name="viewModel">ViewModel закрываемого окна.</param>
        /// <param name="dialogResult">Результат диалога для модального окна или null для обычного.</param>
        public void CloseView(IViewModel viewModel, bool? dialogResult = null)
        {
            if (openViews.ContainsKey(viewModel))
            {
                openViews[viewModel].CloseView(dialogResult);
            }
        }

        /// <summary>
        /// Показывает информационное сообщение в MessageBox.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Показывает сообщение об ошибке в MessageBox.
        /// </summary>
        /// <param name="message">Текст сообщения об ошибке.</param>
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Открывает модальное окно, связывает его с ViewModel и удаляет из словаря после закрытия.
        /// </summary>
        /// <param name="view">Экземпляр окна-наследника BaseView.</param>
        /// <param name="viewModel">ViewModel для данного окна.</param>
        /// <returns>true, если пользователь подтвердил действие; иначе false.</returns>
        private bool ShowModalView(BaseView view, IViewModel viewModel)
        {
            view.BindViewModel(viewModel);

            if (mainView != null && mainView.IsVisible)
            {
                view.Owner = mainView;
            }

            openViews[viewModel] = view;
            bool result = view.ShowDialogView();
            openViews.Remove(viewModel);
            return result;
        }
    }
}

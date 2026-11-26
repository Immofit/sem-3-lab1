using System;
using System.Windows;
using Shared;

namespace Presentation.Wpf
{
    /// <summary>
    /// Базовый класс для всех WPF-представлений (окон) приложения.
    /// Выполняет привязку DataContext к ViewModel, открытие и закрытие окна,
    /// благодаря чему CodeBehind конкретных окон остаётся пустым.
    /// </summary>
    public class BaseView : Window
    {
        /// <summary>
        /// true, если окно открыто модально через ShowDialog; иначе false.
        /// </summary>
        private bool isDialog;

        /// <summary>
        /// Возникает после закрытия окна.
        /// </summary>
        public event Action? ViewClosed;

        /// <summary>
        /// Привязывает переданную ViewModel к окну через свойство DataContext.
        /// </summary>
        /// <param name="viewModel">Экземпляр ViewModel для данного окна.</param>
        public void BindViewModel(IViewModel viewModel)
        {
            DataContext = viewModel;
        }

        /// <summary>
        /// Открывает окно в обычном (немодальном) режиме.
        /// </summary>
        public void ShowView()
        {
            isDialog = false;
            Show();
        }

        /// <summary>
        /// Открывает окно в модальном режиме и возвращает результат диалога.
        /// </summary>
        /// <returns>true, если пользователь подтвердил действие; иначе false.</returns>
        public bool ShowDialogView()
        {
            isDialog = true;
            bool? result = ShowDialog();
            return result == true;
        }

        /// <summary>
        /// Закрывает окно: для модального окна задаёт DialogResult,
        /// для обычного окна вызывает метод Close().
        /// </summary>
        /// <param name="dialogResult">Результат диалога для модального окна или null для обычного.</param>
        public void CloseView(bool? dialogResult = null)
        {
            if (isDialog)
            {
                DialogResult = dialogResult;
            }
            else
            {
                Close();
            }
        }

        /// <summary>
        /// Вызывается при закрытии окна и уведомляет подписчиков через событие ViewClosed.
        /// </summary>
        /// <param name="e">Аргументы события закрытия.</param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (ViewClosed != null)
            {
                ViewClosed();
            }
        }
    }
}

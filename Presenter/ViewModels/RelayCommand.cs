using System;
using System.Windows.Input;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Простая реализация интерфейса ICommand для привязки кнопок WPF к методам ViewModel.
    /// Работает со строго типизированными делегатами Action и Func&lt;bool&gt;.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action execute;
        private Func<bool>? canExecute;

        /// <summary>
        /// Возникает при изменении условий, определяющих доступность команды.
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Создаёт команду с указанным действием и необязательной проверкой доступности.
        /// </summary>
        /// <param name="execute">Метод, который выполняется при вызове команды.</param>
        /// <param name="canExecute">Метод проверки доступности команды (если null — команда всегда доступна).</param>
        public RelayCommand(Action execute, Func<bool>? canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Проверяет, может ли команда выполниться в текущий момент.
        /// Параметр требуется стандартным интерфейсом ICommand и не используется.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        /// <returns>true, если команда доступна; иначе false.</returns>
        public bool CanExecute(object? parameter)
        {
            if (canExecute == null)
            {
                return true;
            }
            return canExecute();
        }

        /// <summary>
        /// Выполняет действие команды.
        /// Параметр требуется стандартным интерфейсом ICommand и не используется.
        /// </summary>
        /// <param name="parameter">Параметр команды (не используется).</param>
        public void Execute(object? parameter)
        {
            execute();
        }

        /// <summary>
        /// Уведомляет интерфейс, что нужно перепроверить доступность команды.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Shared;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Базовый класс для всех ViewModel.
    /// Реализует интерфейс IViewModel (INotifyPropertyChanged)
    /// для уведомления WPF-представления об изменении свойств.
    /// </summary>
    public abstract class BaseViewModel : IViewModel
    {
        /// <summary>
        /// Возникает при изменении значения свойства во ViewModel.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Уведомляет представление об изменении указанного свойства.
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства (подставляется компилятором автоматически).</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

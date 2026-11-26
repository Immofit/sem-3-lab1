using System.ComponentModel;

namespace Shared
{
    /// <summary>
    /// Базовый интерфейс для всех ViewModel.
    /// Позволяет слою Presentation.Wpf привязывать ViewModel к окнам без использования типа object
    /// и без прямой ссылки на проект Presenter.
    /// </summary>
    public interface IViewModel : INotifyPropertyChanged
    {
    }
}

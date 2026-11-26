namespace Presentation.Wpf
{
    /// <summary>
    /// Главное окно WPF-представления.
    /// Вся логика и привязка DataContext вынесены в MainViewModel, BaseView и ViewManager.
    /// </summary>
    public partial class MainWindow : BaseView
    {
        /// <summary>
        /// Инициализирует компоненты главного окна.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}

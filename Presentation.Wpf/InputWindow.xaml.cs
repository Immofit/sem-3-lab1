namespace Presentation.Wpf
{
    /// <summary>
    /// Диалоговое окно ввода одного текстового значения.
    /// Не содержит логики в CodeBehind — вся работа ведётся через InputViewModel.
    /// </summary>
    public partial class InputWindow : BaseView
    {
        /// <summary>
        /// Инициализирует компоненты окна ввода.
        /// </summary>
        public InputWindow()
        {
            InitializeComponent();
        }
    }
}

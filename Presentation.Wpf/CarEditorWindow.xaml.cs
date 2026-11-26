namespace Presentation.Wpf
{
    /// <summary>
    /// Диалоговое окно добавления и редактирования автомобиля.
    /// Не содержит логики в CodeBehind — вся работа ведётся через CarEditorViewModel.
    /// </summary>
    public partial class CarEditorWindow : BaseView
    {
        /// <summary>
        /// Инициализирует компоненты окна редактора машины.
        /// </summary>
        public CarEditorWindow()
        {
            InitializeComponent();
        }
    }
}

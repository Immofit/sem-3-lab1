using ConsoleApp;
using Presentation.WinForms;
using Presentation.Wpf;
using Presenter.ViewModels;

namespace Presenter
{
    /// <summary>
    /// Точка входа приложения: пользователь выбирает представление
    /// (<see cref="ConsoleView"/>, <see cref="MainForm"/> или WPF через MVVM),
    /// после чего создаётся Presenter или ViewModelManager и запускается выбранный режим работы.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входа в программу. Спрашивает у пользователя способ отображения
        /// данных и запускает соответствующее представление.
        /// </summary>
        /// <param name="args">Аргументы командной строки (не используются).</param>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите представление:");
            Console.WriteLine("  1 - Console");
            Console.WriteLine("  2 - WinForms");
            Console.WriteLine("  3 - WPF (MVVM)");
            Console.Write("Ваш выбор: ");

            string? choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    ConsoleView view = new ConsoleView();
                    PresenterFactory.Create(view);
                    view.Run();
                    break;

                case "2":
                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                    MainForm form = new MainForm();
                    PresenterFactory.Create(form);
                    System.Windows.Forms.Application.Run(form);
                    break;

                case "3":
                    System.Windows.Application wpfApp = new System.Windows.Application();
                    ViewManager viewManager = new ViewManager();
                    ViewModelManager viewModelManager = ViewModelFactory.CreateManager(viewManager);
                    viewModelManager.ShowMain();
                    wpfApp.Run();
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}
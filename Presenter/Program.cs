using ConsoleApp;
using Presentation.WinForms;

namespace Presenter
{
    /// <summary>
    /// Точка входа приложения: пользователь выбирает представление
    /// (<see cref="ConsoleView"/> или <see cref="MainForm"/>), после чего
    /// создаётся Presenter и запускается выбранный режим работы.
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
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    MainForm form = new MainForm();
                    PresenterFactory.Create(form);
                    Application.Run(form);
                    break;

                default:
                    Console.WriteLine("Неверный выбор.");
                    break;
            }
        }
    }
}
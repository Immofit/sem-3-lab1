using Presenter;

namespace Presentation.WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var form = new MainForm();
            PresenterFactory.Create(form);
            Application.Run(form);
        }
    }
}
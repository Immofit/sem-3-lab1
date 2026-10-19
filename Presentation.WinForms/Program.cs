using BusinessLogic;
using Ninject;

namespace Presentation.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  Точка входа. Создаёт Logic через DI-контейнер и передаёт её в главную форму.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            IKernel ninjectKernel = new StandardKernel(new SimpleConfigModule());
            Logic logic = ninjectKernel.Get<Logic>();

            Application.Run(new MainForm(logic));
        }
    }
}
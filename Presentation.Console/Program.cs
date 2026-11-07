using Presenter;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var view = new ConsoleView();
            PresenterFactory.Create(view);
            view.Run();
        }
    }
}
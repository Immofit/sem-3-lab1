using BusinessLogic;
using Ninject;
using Ninject.Parameters;
using Shared;

namespace Presenter
{
    /// <summary>
    /// Точка сборки приложения. Создаёт Presenter через Ninject-контейнер:
    /// ILogic и репозиторий внедряются автоматически, View передаётся вручную.
    /// </summary>
    public static class PresenterFactory
    {
        private static IKernel kernel = new StandardKernel(new SimpleConfigModule());

        /// <summary>
        /// Создаёт Presenter для указанного представления.
        /// </summary>
        /// <param name="view">Представление (консоль или форма).</param>
        /// <returns>Готовый Presenter, уже подписанный на события View.</returns>
        public static CarPresenter Create(IView view)
        {
            return kernel.Get<CarPresenter>(new ConstructorArgument("view", view));
        }
    }
}
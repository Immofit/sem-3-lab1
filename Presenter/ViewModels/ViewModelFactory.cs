using Ninject;
using Ninject.Parameters;
using Shared;

namespace Presenter.ViewModels
{
    /// <summary>
    /// Точка сборки MVVM-режима. Создаёт ViewModelManager через Ninject-контейнер:
    /// ILogic и выбранный в SimpleConfigModule репозиторий внедряются автоматически,
    /// а IViewManager передаётся вручную.
    /// </summary>
    public static class ViewModelFactory
    {
        private static IKernel kernel = new StandardKernel(new SimpleConfigModule());

        /// <summary>
        /// Создаёт ViewModelManager, связанный с указанным менеджером представлений.
        /// </summary>
        /// <param name="viewManager">Менеджер представлений WPF.</param>
        /// <returns>Готовый ViewModelManager с внедрённой бизнес-логикой.</returns>
        public static ViewModelManager CreateManager(IViewManager viewManager)
        {
            return kernel.Get<ViewModelManager>(new ConstructorArgument("viewManager", viewManager));
        }
    }
}

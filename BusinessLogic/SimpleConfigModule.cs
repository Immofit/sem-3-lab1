using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Настройка DI-контейнера: говорит Ninject, какую реализацию
    /// подставлять вместо интерфейса IRepository.
    /// Чтобы переключиться между EF и Dapper, нужно поменять только эту связку.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует соответствие «интерфейс → класс». Репозиторий создаётся один раз (Singleton).
        /// </summary>
        public override void Load()
        {
            //Bind<IRepository<Car>>().To<EntityRepository<Car>>().InSingletonScope();

            Bind<IRepository<Car>>()
                .To<DapperRepository<Car>>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", DbSettings.ConnectionString);
        }
    }
}

using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Настройка DI-контейнера: говорит Ninject, какие реализации подставлять вместо интерфейсов.
    /// Чтобы переключиться между EF и Dapper, нужно поменять только этот класс.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует соответствие «интерфейс -> класс». Репозиторий создаётся один раз (Singleton).
        /// </summary>
        public override void Load()
        {
            Bind<IDatabaseInitializer<Car>>()
                .To<SqlServerInitializer<Car>>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", DbSettings.ConnectionString);

            Bind<IRepository<Car>>()
                .To<DapperRepository<Car>>()
                .InSingletonScope()
                .WithConstructorArgument("connectionString", DbSettings.ConnectionString);

            //Bind<AppDbContext<Car>>()
            //    .ToSelf()
            //    .InSingletonScope()
            //    .WithConstructorArgument("connectionString", DbSettings.ConnectionString);
            //
            //Bind<IRepository<Car>>()
            //    .To<EntityRepository<Car>>()
            //    .InSingletonScope();
        }
    }
}
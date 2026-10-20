using DataAccessLayer;
using Model;
using Ninject.Modules;

namespace BusinessLogic
{
    /// <summary>
    /// Модуль Ninject. Здесь, и только здесь, прописано, какой конкретный класс
    /// подставлять вместо интерфейса IRepository. Чтобы переключиться между
    /// EF и Dapper, меняется только этот файл — ни Logic, ни View не трогаются.
    /// </summary>
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует зависимости: какой класс дать, когда кто-то просит IRepository&lt;Car&gt;.
        /// </summary>
        public override void Load()
        {
            // Выбор репозитория меняем только здесь;
            string selected = "EF";

            if (selected == "EF")
            {
                
                Bind<IRepository<Car>>().To<EntityRepository<Car>>().InSingletonScope();
            }
            else
            {
                
                Bind<IRepository<Car>>().To<DapperRepository<Car>>().InSingletonScope()
                    .WithConstructorArgument("connectionString", DbSettings.ConnectionString);
            }
        }
    }
}
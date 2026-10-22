using DataAccessLayer;
using Model;
using Ninject.Modules;

/// <summary>
/// Модуль Ninject. Описывает, какой класс подставлять вместо интерфейса IRepository
/// при создании объектов через контейнер.
/// </summary>
public class SimpleConfigModule : NinjectModule
{
    /// <summary>
    /// Регистрирует в контейнере репозиторий для работы с машинами как Singleton.
    /// Какой именно репозиторий (EF или Dapper) будет использоваться, зависит
    /// от значения переменной selected.
    /// </summary>
    /// <exception cref="Exception">
    /// Выбрасывается, если в selected указано неизвестное название репозитория.
    /// </exception>
    public override void Load()
    {
        // Выбор репозитория меняем только здесь: "EF" или "Dapper"
        string selected = "EF";

        if (selected == "EF")
        {
            Bind<IRepository<Car>>().To<EntityRepository<Car>>().InSingletonScope();
        }
        else if (selected == "Dapper")
        {
            Bind<IRepository<Car>>().To<DapperRepository<Car>>().InSingletonScope()
                .WithConstructorArgument("connectionString", DbSettings.ConnectionString);
        }
        else
        {
            throw new Exception("Неизвестный репозиторий: " + selected);
        }
    }
}
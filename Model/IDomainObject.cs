namespace Model
{
    /// <summary>
    /// Интерфейс для всех сущностей, которые хранятся в базе данных.
    /// Гарантирует, что у сущности есть уникальный идентификатор.
    /// </summary>
    public interface IDomainObject
    {
        /// <summary>
        /// Уникальный идентификатор сущности.
        /// </summary>
        int Id { get; set; }
    }
}

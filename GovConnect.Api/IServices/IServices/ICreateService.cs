namespace IServices;

/// <summary>
/// Defines a generic, asynchronous interface for services that handle the creation of new records in a database.
/// </summary>
/// <typeparam name="T">The type of the entity that represents the record to be created.</typeparam>
/// <typeparam name="TResult">The type of the result object returned by the creation operation.</typeparam>
public interface ICreateService<T, TResult>
{
    /// <summary>
    /// Asynchronously contains the busines logic before insert a new record into the database.
    /// </summary>
    /// <param name="entity">The object containing the data for the new record to insert.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// This task contains the result of the creation operation, or an exception if insertion failed.
    /// The returned result typically contains the newly created object, with any additional properties
    /// assigned after insertion such as ids.
    /// </returns>
    public Task<TResult> CreateAsync(T entity);
}
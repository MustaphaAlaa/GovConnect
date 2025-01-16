namespace IServices;

/// <summary>
/// Defines a generic, asynchronous interface for services that handle the updation of a records in a database.
/// </summary>
/// <typeparam name="TUpdate">The type of the entity that represents updated object contained the data will be updated.</typeparam>
/// <typeparam name="TResult">The type of the result object returned by the updation operation.</typeparam>

public interface IUpdateService<TUpdate, TResult>
{
    /// <summary>
    /// Asynchronously updates a record in the database.
    /// </summary>
    /// <param name="updateRequest">The object containing the data for the record to update.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// This task contains the result of the update operation, or an exception if the update failed.
    /// The returned result typically contains the updated object, with any additional properties
    /// assigned after the update.
    /// </returns>
    Task<TResult> UpdateAsync(TUpdate updateRequest);
}



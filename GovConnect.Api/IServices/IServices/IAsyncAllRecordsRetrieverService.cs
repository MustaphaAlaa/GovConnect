using System.Linq.Expressions;

namespace IServices;

/// <summary>
/// Defines a generic, asynchronous interface for retrieving all records from the database.
/// </summary>
/// <typeparam name="T">The type of the entities representing the records to retrieve.</typeparam>
/// <typeparam name="TResult">The type of the result object returned by the retrieval operation.</typeparam>

public interface IAsyncAllRecordsRetrieverService<T, TResult>
{


    /// <summary>
    /// Asynchronously retrieves all records;
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains all records from the database.
    /// </returns>
    public Task<List<TResult>> GetAllAsync();


    /// <summary>
    /// Asynchronously retrieves all records based on the specified predicate condition.
    /// </summary>
    /// <param name="predicate">An expression that defines the condition for retrieving the records.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains the records that matches the specified predicate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    public Task<IQueryable<TResult>> GetAllAsync(Expression<Func<T, bool>> predicate);
}




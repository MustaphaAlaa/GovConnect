using System.Linq.Expressions;

namespace IServices;



/// <summary>
/// Defines a generic, asynchronous interface for retrieving a specific record from the database.
/// </summary>
/// <typeparam name="T">The type of the entity representing the record to retrieve.</typeparam>
/// <typeparam name="TResult">The type of the result object returned by the retrieval operation.</typeparam>

public interface IAsyncRetrieveService<T, TResult>
{
    /// <summary>
    /// Asynchronously retrieves a record based on the specified predicate condition.
    /// </summary>
    /// <param name="predicate">An expression that defines the condition for retrieving the record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains the record that matches the specified predicate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    Task<TResult> GetByAsync(Expression<Func<T, bool>> predicate);
}

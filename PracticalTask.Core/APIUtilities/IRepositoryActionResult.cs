using System;

namespace PracticalTask.Core.APIUtilities
{
    public interface IRepositoryActionResult<T> where T : class
    {
        T Data { get; set; }
        RepositoryActionStatus Status { get; set; }
        Exception Exception { get; set; }
        string Message { get; set; }

        IRepositoryActionResult<T> GetRepositoryActionResult(Exception exception, string message = null);

        IRepositoryActionResult<T> GetRepositoryActionResult(RepositoryActionStatus status, Exception exception,
            string message = null);

        IRepositoryActionResult<T> GetRepositoryActionResult(RepositoryActionStatus status, string message = null);

        IRepositoryActionResult<T> GetRepositoryActionResult(T result, RepositoryActionStatus status,
            Exception exception, string message = null);

        IRepositoryActionResult<T> GetRepositoryActionResult(T result, RepositoryActionStatus status,
            string message = null);

        IRepositoryActionResult<T> GetRepositoryActionResult(T result);
    }
}

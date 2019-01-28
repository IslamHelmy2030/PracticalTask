using System;

namespace PracticalTask.Core.APIUtilities
{
    public interface IRepositoryActionResult : IRepositoryResult
    {
        Exception Exception { get; set; }
        new RepositoryActionStatus Status { get; set; }
        IRepositoryActionResult GetRepositoryActionResult(Exception exception, string message = null);

        IRepositoryActionResult GetRepositoryActionResult(RepositoryActionStatus status, Exception exception,
            string message = null);

        IRepositoryActionResult GetRepositoryActionResult(RepositoryActionStatus status, string message = null);

        IRepositoryActionResult GetRepositoryActionResult(object result, RepositoryActionStatus status,
            Exception exception, string message = null);

        IRepositoryActionResult GetRepositoryActionResult(object result, RepositoryActionStatus status,
            string message = null);

        IRepositoryActionResult GetRepositoryActionResult(object result);
    }
}

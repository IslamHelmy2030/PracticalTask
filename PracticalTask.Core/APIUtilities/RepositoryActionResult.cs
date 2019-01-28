using System;

namespace PracticalTask.Core.APIUtilities
{
    public class RepositoryActionResult<T> : IRepositoryActionResult<T> where T : class
    {
        public T Data { get; set; }
        public RepositoryActionStatus Status { get; set; }
        public Exception Exception { get; set; }
        public string Message { get; set; }

        public IRepositoryActionResult<T> GetRepositoryActionResult(Exception exception, string message = null)
        {
            return new RepositoryActionResult<T>(exception, message);
        }

        public IRepositoryActionResult<T> GetRepositoryActionResult(RepositoryActionStatus status, Exception exception, string message = null)
        {
            return new RepositoryActionResult<T>(status, exception, message);
        }

        public IRepositoryActionResult<T> GetRepositoryActionResult(RepositoryActionStatus status, string message = null)
        {
            return new RepositoryActionResult<T>(status, message);
        }

        public IRepositoryActionResult<T> GetRepositoryActionResult(T result, RepositoryActionStatus status, Exception exception, string message = null)
        {
            return new RepositoryActionResult<T>(result, status, exception, message);
        }

        public IRepositoryActionResult<T> GetRepositoryActionResult(T result, RepositoryActionStatus status, string message = null)
        {
            return new RepositoryActionResult<T>(result, status, message);
        }

        public IRepositoryActionResult<T> GetRepositoryActionResult(T result)
        {
            return new RepositoryActionResult<T>(result);
        }

        private RepositoryActionResult(RepositoryActionStatus status, Exception exception, string message = null) : this(status)
        {
            Exception = exception;
            Message = message;
        }

        private RepositoryActionResult(Exception exception, string message = null) : this(RepositoryActionStatus.Error)
        {
            Exception = exception;
            Message = message;
        }

        private RepositoryActionResult(RepositoryActionStatus status, string message = null) : this(null, status)
        {
            Status = status;
            Message = message;
        }

        private RepositoryActionResult(T result, RepositoryActionStatus status = RepositoryActionStatus.Ok, string message = null)
        {
            Data = result;
            Status = status;
            Message = message;
        }

        private RepositoryActionResult(T result, RepositoryActionStatus status, Exception exception, string message = null) : this(result, status)
        {
            Exception = exception;
            Message = message;
        }
    }
}

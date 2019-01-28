using System;

namespace PracticalTask.Core.APIUtilities
{
    public class RepositoryActionResult : RepositoryResult, IRepositoryActionResult
    {
        public Exception Exception { get; set; }
        public new RepositoryActionStatus Status { get; set; }
        public IRepositoryActionResult GetRepositoryActionResult(Exception exception, string message = null)
        {
            return new RepositoryActionResult(exception, message);
        }

        public IRepositoryActionResult GetRepositoryActionResult(RepositoryActionStatus status, Exception exception, string message = null)
        {
            return new RepositoryActionResult(status, exception, message);
        }

        public IRepositoryActionResult GetRepositoryActionResult(RepositoryActionStatus status, string message = null)
        {
            return new RepositoryActionResult(status, message);
        }

        public IRepositoryActionResult GetRepositoryActionResult(object result, RepositoryActionStatus status, Exception exception, string message = null)
        {
            return new RepositoryActionResult(result, status, exception, message);
        }

        public IRepositoryActionResult GetRepositoryActionResult(object result, RepositoryActionStatus status, string message = null)
        {
            return new RepositoryActionResult(result, status, message);
        }

        public IRepositoryActionResult GetRepositoryActionResult(object result)
        {
            return new RepositoryActionResult(result);
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

        private RepositoryActionResult(object result, RepositoryActionStatus status = RepositoryActionStatus.Ok, string message = null)
        {
            Data = result;
            Status = status;
            Message = message;
        }

        private RepositoryActionResult(object result, RepositoryActionStatus status, Exception exception, string message = null) : this(result, status)
        {
            Exception = exception;
            Message = message;
        }
    }
}

using System;
using System.Net;
using Microsoft.Extensions.Logging;

namespace PracticalTask.Core.APIUtilities
{
    public class ActionResultResponseHandler : IActionResultResponseHandler
    {
        private readonly IRepositoryResult _repositoryResult;
        private readonly ILogger _logger;

        public ActionResultResponseHandler(IRepositoryResult repositoryResult, ILogger logger)
        {
            _repositoryResult = repositoryResult;
            _logger = logger;
        }
        public IRepositoryResult GetResult(IRepositoryActionResult repositoryActionResult)
        {
            _repositoryResult.Status = GetHttpStatusCode(repositoryActionResult.Status);
            _repositoryResult.Message = repositoryActionResult.Message;
            if (!HasError(repositoryActionResult.Exception))
                _repositoryResult.Data = repositoryActionResult.Data;
            return _repositoryResult;
        }
        private bool HasError(Exception exception)
        {
            if (exception == null) return false;
            _logger.LogError(exception:exception,message:exception.Message);
            return true;
        }
        private HttpStatusCode GetHttpStatusCode(RepositoryActionStatus repositoryActionResult)
        {
            switch (repositoryActionResult)
            {
                case RepositoryActionStatus.Ok: return HttpStatusCode.OK;

                case RepositoryActionStatus.Created: return HttpStatusCode.Created;

                case RepositoryActionStatus.Updated: return HttpStatusCode.Accepted;

                case RepositoryActionStatus.Deleted: return HttpStatusCode.NoContent;

                case RepositoryActionStatus.NothingModified: return HttpStatusCode.NotModified;

                case RepositoryActionStatus.NotFound: return HttpStatusCode.NotFound;

                case RepositoryActionStatus.Error: return HttpStatusCode.InternalServerError;

                default: return HttpStatusCode.BadGateway;
            }
        }
        
    }
}

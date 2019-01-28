using System.Net;

namespace PracticalTask.Core.APIUtilities
{
    public class RepositoryResult : IRepositoryResult
    {
        public object Data { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
    }
}
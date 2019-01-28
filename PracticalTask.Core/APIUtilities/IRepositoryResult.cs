using System.Net;

namespace PracticalTask.Core.APIUtilities
{
    public interface IRepositoryResult
    {
        object Data { get; set; }
        HttpStatusCode Status { get; set; }
        string Message { get; set; }
    }
}
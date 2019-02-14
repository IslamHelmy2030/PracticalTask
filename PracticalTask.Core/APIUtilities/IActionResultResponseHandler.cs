namespace PracticalTask.Core.APIUtilities
{
    public interface IActionResultResponseHandler
    {
        IRepositoryResult GetResult(IRepositoryActionResult repositoryActionResult);
    }
}

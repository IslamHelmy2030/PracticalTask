using Microsoft.AspNetCore.Mvc;
using PracticalTask.Business;
using PracticalTask.Core.APIUtilities;
using System.Threading.Tasks;

namespace PracticalTask.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IActionResultResponseHandler _responseHandler;

        public UserController(IUserBusiness userBusiness, IActionResultResponseHandler responseHandler)
        {
            _userBusiness = userBusiness;
            _responseHandler = responseHandler;
        }

        [HttpGet("GetAllUsers")]
        public async Task<IRepositoryResult> GetAll()
        {
            var repositoryResult = await _userBusiness.GetAllUsers();
            var result = _responseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}

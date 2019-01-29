using Microsoft.AspNetCore.Mvc;
using PracticalTask.Business;
using PracticalTask.Core.APIUtilities;
using System.Threading.Tasks;
using PracticalTask.Business.Dto.Parameter;

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

        [HttpGet("GetUser/{userId}")]
        public async Task<IRepositoryResult> Get(int userId)
        {
            var repositoryResult = await _userBusiness.GetUser(userId);
            var result = _responseHandler.GetResult(repositoryResult);
            return result;
        }

        [HttpPost("AddUsername")]
        public async Task<IRepositoryResult> AddUser([FromBody]UsernameParameterDto user)
        {
            var repositoryResult = await _userBusiness.AddUser(user);
            var result = _responseHandler.GetResult(repositoryResult);
            return result;
        }

        [HttpPut("UpdateUsername")]
        public async Task<IRepositoryResult> UpdateUser([FromBody]UserParameterDto user)
        {
            var repositoryResult = await _userBusiness.UpdateUser(user);
            var result = _responseHandler.GetResult(repositoryResult);
            return result;
        }

        [HttpDelete("DeleteUsername")]
        public async Task<IRepositoryResult> DeleteUser(int userId)
        {
            var repositoryResult = await _userBusiness.DeleteUser(userId);
            var result = _responseHandler.GetResult(repositoryResult);
            return result;
        }
    }
}

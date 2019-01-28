using PracticalTask.Business.Dto;
using PracticalTask.Business.Dto.Parameter;
using PracticalTask.Core.APIUtilities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PracticalTask.Business
{
    public interface IUserBusiness
    {
        Task<IRepositoryActionResult<IList<IUserDto>>> GetAllUsers();
        Task<IRepositoryActionResult<IUserDto>> AddUser(IUsernameParameterDto usernameParameter);
        Task<IRepositoryActionResult<IUserDto>> UpdateUser(IUserParameterDto userParameter);
        Task<IRepositoryActionResult<IUserDto>> DeleteUser(IUserParameterDto userParameter);
    }
}

using PracticalTask.Business.Dto.Parameter;
using PracticalTask.Core.APIUtilities;
using System.Threading.Tasks;

namespace PracticalTask.Business
{
    public interface IUserBusiness
    {
        Task<IRepositoryActionResult> GetAllUsers();
        Task<IRepositoryActionResult> GetUser(int userId);
        Task<IRepositoryActionResult> AddUser(IUsernameParameterDto usernameParameter);
        Task<IRepositoryActionResult> UpdateUser(IUserParameterDto userParameter);
        Task<IRepositoryActionResult> DeleteUser(int userId);
    }
}

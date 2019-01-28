using AutoMapper;
using PracticalTask.Business.Dto;
using PracticalTask.Business.Dto.Parameter;
using PracticalTask.Core.APIUtilities;
using PracticalTask.Data.PracticalDataModel;
using PracticalTask.Repositories.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticalTask.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<User> _unitOfWork;
        private readonly IRepositoryActionResult<IList<IUserDto>> _repositoryActionListResult;
        private readonly IRepositoryActionResult<IUserDto> _repositoryActionResult;
        public UserBusiness(IMapper mapper, IUnitOfWork<User> unitOfWork, IRepositoryActionResult<IList<IUserDto>> repositoryActionListResult, IRepositoryActionResult<IUserDto> repositoryActionResult)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repositoryActionListResult = repositoryActionListResult;
            _repositoryActionResult = repositoryActionResult;
        }

        public async Task<IRepositoryActionResult<IList<IUserDto>>> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Repo.GetAll();
                if (!users.Any())
                {
                    return _repositoryActionListResult.GetRepositoryActionResult(RepositoryActionStatus.NotFound);
                }

                var userDtos = _mapper.Map<IList<User>, IList<IUserDto>>(users);
                return _repositoryActionListResult.GetRepositoryActionResult(userDtos);
            }
            catch (Exception e)
            {
                return _repositoryActionListResult.GetRepositoryActionResult(e, "Something went error");
            }
        }

        public async Task<IRepositoryActionResult<IUserDto>> AddUser(IUsernameParameterDto usernameParameter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usernameParameter?.Username))
                {
                    return _repositoryActionResult.GetRepositoryActionResult(exception: null, message: "Invalid Username");
                }
                var user = _mapper.Map<IUsernameParameterDto, User>(usernameParameter);
                var newUser = _unitOfWork.Repo.Add(user);
                var savingCount = await _unitOfWork.SaveChanges();
                if (savingCount == 0)
                    return _repositoryActionResult.GetRepositoryActionResult(RepositoryActionStatus.NothingModified, "Nothing was Added");
                var userDto = _mapper.Map<User, IUserDto>(newUser);
                return _repositoryActionResult.GetRepositoryActionResult(result: userDto,
                    status: RepositoryActionStatus.Created, message: "Saved Successfully");
            }
            catch (Exception e)
            {
                return _repositoryActionResult.GetRepositoryActionResult(e, "Something went error");
            }
        }

        public async Task<IRepositoryActionResult<IUserDto>> UpdateUser(IUserParameterDto userParameter)
        {
            return await UpdateOrDeleteUser(userParameter: userParameter, isDeleteAction: false);
        }

        public async Task<IRepositoryActionResult<IUserDto>> DeleteUser(IUserParameterDto userParameter)
        {
            return await UpdateOrDeleteUser(userParameter: userParameter, isDeleteAction: true);
        }

        private async Task<IRepositoryActionResult<IUserDto>> UpdateOrDeleteUser(IUserParameterDto userParameter,bool isDeleteAction)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userParameter?.Username))
                {
                    return _repositoryActionResult.GetRepositoryActionResult(exception: null, message: "Invalid Username");
                }
                var user = _mapper.Map<IUserParameterDto, User>(userParameter);
                if (isDeleteAction) user.IsActive = false;
                _unitOfWork.Repo.Update(user);
                var savingCount = await _unitOfWork.SaveChanges();
                if (savingCount == 0)
                    return _repositoryActionResult.GetRepositoryActionResult(RepositoryActionStatus.NothingModified, "Nothing was Seved");
                return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.Updated, message: "Saved Successfully");
            }
            catch (Exception e)
            {
                return _repositoryActionResult.GetRepositoryActionResult(e, "Something went error");
            }
        }

    }
}

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
        private readonly IRepositoryActionResult _repositoryActionListResult;
        private readonly IRepositoryActionResult _repositoryActionResult;

        public UserBusiness(IMapper mapper, IUnitOfWork<User> unitOfWork, IRepositoryActionResult repositoryActionListResult, IRepositoryActionResult repositoryActionResult)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repositoryActionListResult = repositoryActionListResult;
            _repositoryActionResult = repositoryActionResult;
        }

        public async Task<IRepositoryActionResult> GetAllUsers()
        {
            try
            {
                var users = await _unitOfWork.Repo.Find(x => x.IsActive);
                if (!users.Any())
                {
                    return _repositoryActionListResult.GetRepositoryActionResult(RepositoryActionStatus.NotFound);
                }
                var userDtos = _mapper.Map<IList<User>, IList<IUserDto>>(users);
                return _repositoryActionListResult.GetRepositoryActionResult(userDtos,RepositoryActionStatus.Ok);
            }
            catch (Exception e)
            {
                return _repositoryActionListResult.GetRepositoryActionResult(exception:e,message: "Something went error",status:RepositoryActionStatus.Error);
            }
        }

        public async Task<IRepositoryActionResult> GetUser(int userId)
        {
            try
            {
                var user = await _unitOfWork.Repo.FirstOrDefault(x => x.Id == userId && x.IsActive);
                if (user== null)
                {
                    return _repositoryActionListResult.GetRepositoryActionResult(RepositoryActionStatus.NotFound);
                }
                var userDto = _mapper.Map<User, IUserDto>(user);
                return _repositoryActionListResult.GetRepositoryActionResult(userDto, RepositoryActionStatus.Ok);
            }
            catch (Exception e)
            {
                return _repositoryActionListResult.GetRepositoryActionResult(exception: e, message: "Something went error", status: RepositoryActionStatus.Error);
            }
        }

        public async Task<IRepositoryActionResult> AddUser(IUsernameParameterDto usernameParameter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usernameParameter?.Username))
                {
                    return _repositoryActionResult.GetRepositoryActionResult(message: "Invalid Username");
                }
                var user = _mapper.Map<IUsernameParameterDto, User>(usernameParameter);
                var newUser = _unitOfWork.Repo.Add(user);
                var savingCount = await _unitOfWork.SaveChanges();
                if (savingCount == 0)
                    return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.NothingModified,message: "Nothing was Added");
                var userDto = _mapper.Map<User, IUserDto>(newUser);
                return _repositoryActionResult.GetRepositoryActionResult(result: userDto,
                    status: RepositoryActionStatus.Created, message: "Saved Successfully");
            }
            catch (Exception e)
            {
                return _repositoryActionResult.GetRepositoryActionResult(exception: e,message: "Something went error");
            }
        }

        public async Task<IRepositoryActionResult> UpdateUser(IUserParameterDto userParameter)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userParameter?.Username))
                {
                    return _repositoryActionResult.GetRepositoryActionResult(exception: null, message: "Invalid Username");
                }
                var user = _mapper.Map<IUserParameterDto, User>(userParameter);
                _unitOfWork.Repo.Update(user);
                var savingCount = await _unitOfWork.SaveChanges();
                if (savingCount == 0)
                    return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.NothingModified, message: "Nothing was Seved");
                return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.Updated, message: "Saved Successfully");
            }
            catch (Exception e)
            {
                return _repositoryActionResult.GetRepositoryActionResult(exception: e, message: "Something went error");
            }
        }

        public async Task<IRepositoryActionResult> DeleteUser(int userId)
        {
            try
            {
                var user = await _unitOfWork.Repo.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                    return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.NothingModified, message: "Nothing was Seved");
                user.IsActive = false;
                _unitOfWork.Repo.Update(user);
                var savingCount = await _unitOfWork.SaveChanges();
                if (savingCount == 0)
                    return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.NothingModified, message: "Nothing was Seved");
                return _repositoryActionResult.GetRepositoryActionResult(status: RepositoryActionStatus.Updated, message: "Saved Successfully");
            }
            catch (Exception e)
            {
                return _repositoryActionResult.GetRepositoryActionResult(exception: e, message: "Something went error");
            }
        }

        

    }
}

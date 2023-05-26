
using WebAPI.Models;
using WebAPI.Models.Entities;

namespace WebAPI.Services.UserService
{
    public class UserService : IUserService
    {

        private static List<User> _users = new List<User>
        {
            new User(),
            new User {UserId=2, UserName="Alexis"}
        };
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetUserDTO>>> GetAllUsers()
        {

            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            serviceResponse.Data = _users.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> GetUserById(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDTO>();
            var user = _users.FirstOrDefault(user => user.UserId == id);
            serviceResponse.Data = _mapper.Map<GetUserDTO>(user);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDTO>>> AddUser(AddUserDTO newUser)
        {
            
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            var usuario = _mapper.Map<User>(newUser);
            usuario.UserId = _users.Max(u => u.UserId) + 1;
            _users.Add(usuario);
            serviceResponse.Data = _users.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDTO>> UpdateUser(UpdateUserDTO updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDTO>();
            try
            {
                var user = _users.FirstOrDefault(u => u.UserId == updatedUser.UserId);
                if (user is null) throw new Exception($"User with ID '{updatedUser.UserId}' not found.");

                _mapper.Map(updatedUser, user);

                user.UserName = updatedUser.UserName;
                user.PasswordHash = updatedUser.PasswordHash;
                user.UserType = updatedUser.UserType;
                user.IsDeleted = updatedUser.IsDeleted;
                serviceResponse.Data = _mapper.Map<GetUserDTO>(user);
            }
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;


        }

        public async Task<ServiceResponse<List<GetUserDTO>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDTO>>();
            try
            {
                var user = _users.FirstOrDefault(u => u.UserId == id);
                if (user is null) throw new Exception($"User with ID '{id}' not found.");

                _users.Remove(user);
                serviceResponse.Data= _users.Select(user => _mapper.Map<GetUserDTO>(user)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}

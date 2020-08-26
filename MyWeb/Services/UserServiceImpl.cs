using System;
using System.Threading.Tasks;
using MyWeb.Repositories;
using MyWeb.ViewModels.User;

namespace MyWeb.Services
{
    public class UserServiceImpl : IUserService
    {
        protected IUserRepository UserRepository { get; set; }

        public UserServiceImpl(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public async Task<UserDO> login(string username, string password)
        {
            return await UserRepository.Login(username, password);
        }
    }
}

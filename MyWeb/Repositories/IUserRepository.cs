using System;
using System.Threading.Tasks;
using MyWeb.ViewModels.User;

namespace MyWeb.Repositories
{
    public interface IUserRepository
    {

        Task<UserDO> Login(string username, string password);

        Task<UserDO> GetById(int id);

    }
}

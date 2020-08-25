using System;
using System.Threading.Tasks;
using MyWeb.ViewModels.User;

namespace MyWeb.Repositories
{
    public interface IUserRepository
    {

        Task<UserDO> login(string username, string password);

    }
}

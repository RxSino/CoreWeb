using System;
using System.Threading.Tasks;
using MyWeb.ViewModels.User;

namespace MyWeb.Services
{
    public interface IUserService
    {

        Task<UserDO> login(string username, string password);

    }
}

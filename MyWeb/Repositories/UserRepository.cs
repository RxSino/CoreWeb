using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MyWeb.utils;
using MyWeb.ViewModels.User;

namespace MyWeb.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IDapperConfiguration configuration) : base(configuration)
        {
        }

        public async Task<UserDO> login(string username, string password)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from ums_user u where u.enabled=1 and u.username=@username and u.password=@password");
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@username", username);
            parameters.Add("@password", password);
            using (ReadConnection)
            {
                var result = await ReadConnection.QueryAsync<UserDO>(sql.ToString(), parameters);


                return result.FirstOrDefault();
            }
        }

    }
}

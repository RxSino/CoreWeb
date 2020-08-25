using System;
namespace MyWeb.ViewModels.User
{
    public class UserDO
    {
        public UserDO()
        {
        }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PhonePrefix { get; set; }

        public string PhoneNumber { get; set; }

        public long RoleId { get; set; }

    }
}

using System;
using Dapper.Contrib.Extensions;

namespace MyWeb.ViewModels.User
{
    [Table("ums_user")]
    public class UserDO
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PhonePrefix { get; set; }

        public string PhoneNumber { get; set; }

        public long RoleId { get; set; }

    }
}

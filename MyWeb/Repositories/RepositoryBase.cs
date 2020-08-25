using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using MyWeb.utils;

namespace MyWeb.Repositories
{
    public class RepositoryBase
    {
        public RepositoryBase(IDapperConfiguration configuration)
        {
            Configuration = configuration;
            WriteConnection = new MySqlConnection(configuration.WriteConnectionString);
            ReadConnection = new MySqlConnection(configuration.ReadConnectionStrign);
        }

        protected IDapperConfiguration Configuration { get; set; }

        protected IDbConnection ReadConnection { get; set; }

        protected IDbConnection WriteConnection { get; set; }

    }
}

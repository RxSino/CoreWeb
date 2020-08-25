using System;
namespace MyWeb.utils
{
    public class DapperConfiguration : IDapperConfiguration
    {
        public string WriteConnectionString { get; set; }

        public string ReadConnectionStrign { get; set; }

    }
}

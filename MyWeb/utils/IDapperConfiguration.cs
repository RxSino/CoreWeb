using System;
namespace MyWeb.utils
{
    public interface IDapperConfiguration
    {
        string WriteConnectionString { get; set; }

        string ReadConnectionStrign { get; set; }

    }
}

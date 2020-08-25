using System;
namespace MyWeb.ViewModels
{
    public class BaseResposne<T>
    {

        public int Code
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }

        public T Data
        {
            get;
            set;
        }
    }
}

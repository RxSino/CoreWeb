using System;
namespace MyWeb.ViewModels
{
    public class RawResponse
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

        public object Data
        {
            get;
            set;
        }
    }
}

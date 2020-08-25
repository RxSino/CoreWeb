using System;
namespace MyWeb.Errors
{
    public class MyException : Exception
    {
        public MyException()
        {
        }

        public MyException(string value)
        {
            Value = value;
        }

        public MyException(int code, string value)
        {
            Code = code;
            Value = value;
        }

        public int Code { get; set; } = -1;

        public string Value { get; set; }

    }
}

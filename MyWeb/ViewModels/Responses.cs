using System;
namespace MyWeb.ViewModels
{
    public class Responses
    {
        public static RawResponse Success()
        {
            return new RawResponse
            {
                Code = 0,
                Message = "success"
            };
        }

        public static RawResponse Success(object data)
        {
            return new RawResponse
            {
                Code = 0,
                Message = "success",
                Data = data
            };
        }

        public static RawResponse Error()
        {
            return new RawResponse
            {
                Code = -1,
                Message = "error"
            };
        }

        public static RawResponse TokenError()
        {
            return new RawResponse
            {
                Code = -2,
                Message = "token error"
            };
        }

        public static RawResponse ValidationError()
        {
            return new RawResponse
            {
                Code = -3,
                Message = "validation error"
            };
        }

        public static RawResponse Error(int code, string message)
        {
            return new RawResponse
            {
                Code = code,
                Message = message
            };
        }

        public static BaseResposne<T> Success<T>()
        {
            return new BaseResposne<T>
            {
                Code = 0,
                Message = "success"
            };
        }

        public static BaseResposne<T> Success<T>(T data)
        {
            return new BaseResposne<T>
            {
                Code = 0,
                Message = "success",
                Data = data
            };
        }

        public static BaseResposne<T> Error<T>()
        {
            return new BaseResposne<T>
            {
                Code = -1,
                Message = "error"
            };
        }

        public static BaseResposne<T> Error<T>(int code, string message)
        {
            return new BaseResposne<T>
            {
                Code = code,
                Message = message
            };
        }

    }
}

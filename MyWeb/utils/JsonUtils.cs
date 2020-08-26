using System;
using System.Text.Json;

namespace MyWeb.utils
{
    public class JsonUtils
    {
        public static JsonSerializerOptions DefaultOptions()
        {
            return new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true
            };
        }

    }
}

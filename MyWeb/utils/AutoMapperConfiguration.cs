using System;
using AutoMapper;
using MyWeb.Models;

namespace MyWeb.utils
{
    public class AutoMapperConfiguration
    {

        public static void Initialize(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<TodoItem, TodoItemDTO>();
        }

    }
}

using System;
using AutoMapper;
using MyWeb.Models;

namespace MyWeb.utils
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<TodoItem, TodoItemDTO>()
                .ForMember(vm => vm.OtherName, map => map.MapFrom(m => m.Name));



        }
    }
}

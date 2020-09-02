using AutoMapper;
using NetMap.Data.Models;
using NetMap.Web.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetMap.Web.Profiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<User, UserDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}

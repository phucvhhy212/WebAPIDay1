using AutoMapper;

namespace WebApi
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<WebApi.Models.TaskDto, DataAccess.Task>().ReverseMap();
        }
    }
}

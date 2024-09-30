using AutoMapper;
using TestTask.Domain.Models;

namespace TestTask.DtoModels.Mapping
{
    public class TimerWebHookMappingProfile:Profile
    {
        public TimerWebHookMappingProfile()
        {
            CreateMap<TimerWebHookDto, TimerWebHook>().ReverseMap();
        }
    }
}

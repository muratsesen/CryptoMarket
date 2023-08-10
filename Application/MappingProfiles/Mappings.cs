using Application.Models;
using AutoMapper;
using Domain;

namespace Application.MappingProfiles
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<NewUser, User>();
            CreateMap<User, UserDto>();

            CreateMap<NewInstruction, Instruction>();
            CreateMap<Instruction, InstructionDto>();

            CreateMap<NewNotification, Notification>();
            CreateMap<Notification, NotificationDto>();
        }
    }
}

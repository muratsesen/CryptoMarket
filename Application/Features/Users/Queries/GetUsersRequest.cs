using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetUsersRequest : IRequest<List<UserDto>>
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetUsersRequest()
        {
            //CacheKey = "GetUsers";
        }
    }

    public class GetUsersRequestHandler : IRequestHandler<GetUsersRequest, List<UserDto>>
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public GetUsersRequestHandler(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            List<User> users = await _userRepo.GetAllAsync();

            if (users != null)
            {
                List<UserDto> userDtos = _mapper.Map<List<UserDto>>(users);
                return userDtos;
            }
            return null;
        }
    }
}

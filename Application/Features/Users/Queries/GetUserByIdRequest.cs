using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdRequest : IRequest<UserDto>, ICacheable
    {
        public int UserId { get; set; }
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; }
        public TimeSpan? SlidingExpiration { get; set; }

        public GetUserByIdRequest(int userId)
        {
            UserId = userId;
            CacheKey = $"GetUserById:{UserId}";
        }
    }

    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, UserDto>
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public GetUserByIdRequestHandler(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            User userInDb = await _userRepo.GetByIdAsync(request.UserId);

            if (userInDb != null)
            {
                UserDto userDto = _mapper.Map<UserDto>(userInDb);
                return userDto;
            }
            return null;
        }
    }
}

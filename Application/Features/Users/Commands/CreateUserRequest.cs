using Application.Models;
using Application.PipelineBehaviours.Contracts;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class CreateUserRequest : IRequest<bool>, IValidatable
    {
        public NewUser UserRequest { get; set; }

        public CreateUserRequest(NewUser newUserRequest)
        {
            UserRequest = newUserRequest;
        }
    }

    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, bool>
    {
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public CreateUserRequestHandler(IUserRepo userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request.UserRequest);
            await _userRepo.AddNewAsync(user);

            return true;
        }
    }
}

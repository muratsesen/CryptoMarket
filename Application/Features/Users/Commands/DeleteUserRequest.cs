using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class DeleteUserRequest : IRequest<bool>
    {
        public int UserId { get; set; }

        public DeleteUserRequest(int userId)
        {
            UserId = userId;
        }
    }

    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, bool>
    {
        private readonly IUserRepo _userRepo;

        public DeleteUserRequestHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            User userInDb = await _userRepo.GetByIdAsync(request.UserId);

            if (userInDb != null)
            {
                await _userRepo.DeleteAsync(userInDb);
                return true;
            }
            return false;
        }
    }
}

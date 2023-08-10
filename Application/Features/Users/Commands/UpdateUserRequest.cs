using Application.Models;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Users.Commands
{
    public class UpdateUserRequest : IRequest<bool>
    {
        public UpdateUser UpdateUser { get; set; }

        public UpdateUserRequest(UpdateUser updateUser)
        {
            UpdateUser = updateUser;
        }
    }

    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, bool>
    {
        private readonly IUserRepo _userRepo;

        public UpdateUserRequestHandler(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<bool> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            User userInDb = await _userRepo.GetByIdAsync(request.UpdateUser.Id);
            
            if (userInDb != null)
            {
                userInDb.Name = request.UpdateUser.Name;

                await _userRepo.UpdateAsync(userInDb);
                return true;
            }
            return false;
        }
    }
}

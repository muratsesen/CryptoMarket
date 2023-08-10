using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Notifications.Commands
{
    public class CreateNotificationRequest : IRequest<bool>
    {
        public NewNotification NewNotification { get; set; }
        public CreateNotificationRequest(NewNotification newNotification)
        {
            NewNotification = newNotification;
        }
    }

    public class CreateNotificationRequestHandler : IRequestHandler<CreateNotificationRequest, bool>
    {
        private readonly INotificationRepo _notificationRepo;
        private readonly IMapper _mapper;

        public CreateNotificationRequestHandler(INotificationRepo notificationRepo, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateNotificationRequest request, CancellationToken cancellationToken)
        {
            Notification notification = _mapper.Map<Notification>(request.NewNotification);

            await _notificationRepo.AddNewAsync(notification);
            return true;
        }
    }
}

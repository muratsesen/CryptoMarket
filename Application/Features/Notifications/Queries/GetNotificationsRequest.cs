using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Notifications.Queries
{
    public class GetNotificationsRequest : IRequest<List<NotificationDto>>
    {
    }

    public class GetNotificationsRequestHandler : IRequestHandler<GetNotificationsRequest, List<NotificationDto>>
    {
        private readonly INotificationRepo _notificationRepo;
        private readonly IMapper _mapper;

        public GetNotificationsRequestHandler(INotificationRepo notificationRepo, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }

        public async Task<List<NotificationDto>> Handle(GetNotificationsRequest request, CancellationToken cancellationToken)
        {
            List<Notification> notifications = await _notificationRepo.GetAllAsync();
            if (notifications != null)
            {
                List<NotificationDto> notificationDtos = _mapper.Map<List<NotificationDto>>(notifications);
                return notificationDtos;
            }
            return null;
        }
    }
}

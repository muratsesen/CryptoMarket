using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Notifications.Queries
{
    public class GetNotificationByIdRequest : IRequest<NotificationDto>
    {
        public int NotificationId { get; set; }

        public GetNotificationByIdRequest(int notificationId)
        {
            NotificationId = notificationId;
        }
    }

    public class GetNotificationByIdRequestHandler : IRequestHandler<GetNotificationByIdRequest, NotificationDto>
    {
        private readonly INotificationRepo _notificationRepo;
        private readonly IMapper _mapper;

        public GetNotificationByIdRequestHandler(INotificationRepo notificationRepo, IMapper mapper)
        {
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }

        public async Task<NotificationDto> Handle(GetNotificationByIdRequest request, CancellationToken cancellationToken)
        {
            Notification notificationInDb = await _notificationRepo.GetByIdAsync(request.NotificationId);
            if (notificationInDb != null)
            {
                NotificationDto notificationDto = _mapper.Map<NotificationDto>(notificationInDb);
                return notificationDto;
            }
            return null;
        }
    }
}

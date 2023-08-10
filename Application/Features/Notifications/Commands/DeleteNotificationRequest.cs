using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Notifications.Commands
{
    public class DeleteNotificationRequest : IRequest<bool>
    {
        public int NotificationId { get; set; }

        public DeleteNotificationRequest(int notificationId)
        {
            NotificationId = notificationId;
        }
    }

    public class DeleteNotificationRequestHandler : IRequestHandler<DeleteNotificationRequest, bool>
    {
        private readonly INotificationRepo _notificationRepo;

        public DeleteNotificationRequestHandler(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<bool> Handle(DeleteNotificationRequest request, CancellationToken cancellationToken)
        {
            Notification notificationInDb = await _notificationRepo.GetByIdAsync(request.NotificationId);
            if (notificationInDb != null)
            {
                await _notificationRepo.DeleteAsync(notificationInDb);
                return true;
            }
            return false;
        }
    }
}

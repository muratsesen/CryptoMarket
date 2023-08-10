using Application.Models;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Notifications.Commands
{
    public class UpdateNotificationRequest : IRequest<bool>
    {
        public UpdateNotification UpdateNotification { get; set; }

        public UpdateNotificationRequest(UpdateNotification updateNotification)
        {
            UpdateNotification = updateNotification;
        }
    }

    public class UpdateNotificationRequestHandler : IRequestHandler<UpdateNotificationRequest, bool>
    {
        private readonly INotificationRepo _notificationRepo;

        public UpdateNotificationRequestHandler(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<bool> Handle(UpdateNotificationRequest request, CancellationToken cancellationToken)
        {
            Notification notificationInDb = await _notificationRepo.GetByIdAsync(request.UpdateNotification.Id);
            if (notificationInDb != null)
            {
                notificationInDb.ChannelType = request.UpdateNotification.ChannelType;

                await _notificationRepo.UpdateAsync(notificationInDb);
                return true;
            }
            return false;
        }
    }
}

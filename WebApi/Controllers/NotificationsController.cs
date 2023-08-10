using Application.Features.Notifications.Commands;
using Application.Features.Notifications.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public NotificationsController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewNotification([FromBody] NewNotification newNotification)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreateNotificationRequest(newNotification));
            if (isSuccessful)
            {
                return Ok("Notification created successfully.");
            }
            return BadRequest("Failed to create notification.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateNotification(UpdateNotification updateNotification)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateNotificationRequest(updateNotification));
            if (isSuccessful)
            {
                return Ok("Notification updated successfully.");
            }
            return NotFound("Notification does not exists.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotification(int id)
        {
            bool isSuccessfull = await _mediatrSender.Send(new DeleteNotificationRequest(id));
            if (isSuccessfull)
            {
                return Ok("Notification deleted successfully.");
            }
            return NotFound("Notification does not exists.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id)
        {
            NotificationDto notification = await _mediatrSender.Send(new GetNotificationByIdRequest(id));
            if(notification != null)
            {
                return Ok(notification);
            }
            return NotFound("Notification does not exists.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetNotifications()
        {
            List<NotificationDto> notifications = await _mediatrSender.Send(new GetNotificationsRequest());
            if (notifications != null)
            {
                return Ok(notifications);
            }
            return NotFound("No Notifications were found.");
        }
    }
}

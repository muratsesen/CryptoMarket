using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Instructions.Commands
{
    public class CreateInstructionRequest : IRequest<bool>
    {
        public NewInstruction NewInstruction { get; set; }
        public CreateInstructionRequest(NewInstruction newInstruction)
        {
            NewInstruction = newInstruction;
        }
    }

    public class CreateInstructionRequestHandler : IRequestHandler<CreateInstructionRequest, bool>
    {
        private readonly IInstructionRepo _instructionRepo;
        private readonly INotificationRepo _notificationRepo;
        private readonly IMapper _mapper;

        public CreateInstructionRequestHandler(IInstructionRepo instructionRepo,INotificationRepo notificationRepo, IMapper mapper)
        {
            _instructionRepo = instructionRepo;
            _notificationRepo = notificationRepo;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CreateInstructionRequest request, CancellationToken cancellationToken)
        {
            Instruction instruction = _mapper.Map<Instruction>(request.NewInstruction);

            //TODO use ef interceptor

            
            var savedInstruction = await _instructionRepo.AddNewAsync(instruction);
            if(savedInstruction != null)
            {
                foreach(var notification in request.NewInstruction.Notificaitons)
                {
                    Notification notification1 = new Notification();
                    notification1.InstructionId = savedInstruction.Id;
                    notification1.ChannelType = notification.ChannelType;
                    
                    await _notificationRepo.AddNewAsync(notification1);
                }
            }
            
            return true;
        }
    }
}

using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Instructions.Commands
{
    public class DeleteInstructionRequest : IRequest<bool>
    {
        public int InstructionId { get; set; }

        public DeleteInstructionRequest(int instructionId)
        {
            InstructionId = instructionId;
        }
    }

    public class DeleteInstructionRequestHandler : IRequestHandler<DeleteInstructionRequest, bool>
    {
        private readonly IInstructionRepo _instructionRepo;

        public DeleteInstructionRequestHandler(IInstructionRepo instructionRepo)
        {
            _instructionRepo = instructionRepo;
        }

        public async Task<bool> Handle(DeleteInstructionRequest request, CancellationToken cancellationToken)
        {
            Instruction instructionInDb = await _instructionRepo.GetByIdAsync(request.InstructionId);
            if (instructionInDb != null)
            {
                await _instructionRepo.DeleteAsync(instructionInDb);
                return true;
            }
            return false;
        }
    }
}

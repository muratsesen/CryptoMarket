using Application.Models;
using Application.Repositories;
using Domain;
using MediatR;

namespace Application.Features.Instructions.Commands
{
    public class UpdateInstructionRequest : IRequest<bool>
    {
        public UpdateInstruction UpdateInstruction { get; set; }

        public UpdateInstructionRequest(UpdateInstruction updateInstruction)
        {
            UpdateInstruction = updateInstruction;
        }
    }

    public class UpdateInstructionRequestHandler : IRequestHandler<UpdateInstructionRequest, bool>
    {
        private readonly IInstructionRepo _instructionRepo;

        public UpdateInstructionRequestHandler(IInstructionRepo instructionRepo)
        {
            _instructionRepo = instructionRepo;
        }

        public async Task<bool> Handle(UpdateInstructionRequest request, CancellationToken cancellationToken)
        {
            Instruction instructionInDb = await _instructionRepo.GetByIdAsync(request.UpdateInstruction.Id);
            if (instructionInDb != null)
            {
                instructionInDb.IsDone = request.UpdateInstruction.IsDone;
                instructionInDb.DayOfMonth = request.UpdateInstruction.DayOfMonth;
                instructionInDb.Amount = request.UpdateInstruction.DayOfMonth;

                await _instructionRepo.UpdateAsync(instructionInDb);
                return true;
            }
            return false;
        }
    }
}

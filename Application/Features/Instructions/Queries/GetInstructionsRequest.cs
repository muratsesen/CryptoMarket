using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Instructions.Queries
{
    public class GetInstructionsRequest : IRequest<List<InstructionDto>>
    {
    }

    public class GetInstructionsRequestHandler : IRequestHandler<GetInstructionsRequest, List<InstructionDto>>
    {
        private readonly IInstructionRepo _instructionRepo;
        private readonly IMapper _mapper;

        public GetInstructionsRequestHandler(IInstructionRepo instructionRepo, IMapper mapper)
        {
            _instructionRepo = instructionRepo;
            _mapper = mapper;
        }

        public async Task<List<InstructionDto>> Handle(GetInstructionsRequest request, CancellationToken cancellationToken)
        {
            List<Instruction> instructions = await _instructionRepo.GetAllAsync();
            if (instructions != null)
            {
                List<InstructionDto> instructionDtos = _mapper.Map<List<InstructionDto>>(instructions);
                return instructionDtos;
            }
            return null;
        }
    }
}

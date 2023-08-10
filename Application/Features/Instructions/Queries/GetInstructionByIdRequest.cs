using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Instructions.Queries
{
    public class GetInstructionByIdRequest : IRequest<InstructionDto>
    {
        public int InstructionId { get; set; }

        public GetInstructionByIdRequest(int instructionId)
        {
            InstructionId = instructionId;
        }
    }

    public class GetInstructionByIdRequestHandler : IRequestHandler<GetInstructionByIdRequest, InstructionDto>
    {
        private readonly IInstructionRepo _instructionRepo;
        private readonly IMapper _mapper;

        public GetInstructionByIdRequestHandler(IInstructionRepo instructionRepo, IMapper mapper)
        {
            _instructionRepo = instructionRepo;
            _mapper = mapper;
        }

        public async Task<InstructionDto> Handle(GetInstructionByIdRequest request, CancellationToken cancellationToken)
        {
            Instruction instructionInDb = await _instructionRepo.GetByIdAsync(request.InstructionId);
            if (instructionInDb != null)
            {
                InstructionDto instructionDto = _mapper.Map<InstructionDto>(instructionInDb);
                return instructionDto;
            }
            return null;
        }
    }
}

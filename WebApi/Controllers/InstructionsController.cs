using Application.Features.Instructions.Commands;
using Application.Features.Instructions.Queries;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructionsController : ControllerBase
    {
        private readonly ISender _mediatrSender;

        public InstructionsController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewInstruction([FromBody] NewInstruction newInstruction)
        {
            bool isSuccessful = await _mediatrSender.Send(new CreateInstructionRequest(newInstruction));
            if (isSuccessful)
            {
                return Ok("Instruction created successfully.");
            }
            return BadRequest("Failed to create instruction.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateInstruction(UpdateInstruction updateInstruction)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdateInstructionRequest(updateInstruction));
            if (isSuccessful)
            {
                return Ok("Instruction updated successfully.");
            }
            return NotFound("Instruction does not exists.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstruction(int id)
        {
            bool isSuccessfull = await _mediatrSender.Send(new DeleteInstructionRequest(id));
            if (isSuccessfull)
            {
                return Ok("Instruction deleted successfully.");
            }
            return NotFound("Instruction does not exists.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstruction(int id)
        {
            InstructionDto instruction = await _mediatrSender.Send(new GetInstructionByIdRequest(id));
            if(instruction != null)
            {
                return Ok(instruction);
            }
            return NotFound("Instruction does not exists.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetInstructions()
        {
            List<InstructionDto> instructions = await _mediatrSender.Send(new GetInstructionsRequest());
            if (instructions != null)
            {
                return Ok(instructions);
            }
            return NotFound("No Instructions were found.");
        }
    }
}

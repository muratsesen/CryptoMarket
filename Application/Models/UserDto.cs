﻿namespace Application.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<InstructionDto> Images { get; set; }
    }
}

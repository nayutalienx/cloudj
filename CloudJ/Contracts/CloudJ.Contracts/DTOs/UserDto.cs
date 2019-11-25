
using CloudJ.Contracts.DTOs.SolutionDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudJ.Contracts.DTOs
{
    public class UserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public SolutionDto[] PurchasedSolutions { get; set; }
        public SolutionDto[] PostedSolutions { get; set; }
        public SolutionDto[] FavoriteSolutions { get; set; }
    } 
}

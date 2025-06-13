using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NZWalks.Models.Domain;

namespace NZWalks.Models.DTOs
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        
        //Navigation property
        public DifficultyDto Difficulty { get; set; }
        public RegionDto Region { get; set; }
    }
}
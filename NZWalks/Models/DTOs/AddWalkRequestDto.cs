using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NZWalks.Models.DTOs
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Max length should be 10 bytes")]
        [MinLength(3, ErrorMessage = "Minimum length should be 3 bytes")]
        public string Name { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Minimum length should be 100 bytes")]
        public string Description { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Max length should be 10 bytes")]
        public string lengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }
        [Required]
        public Guid RegionId { get; set; }
    }
}
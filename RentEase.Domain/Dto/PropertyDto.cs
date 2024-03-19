using Microsoft.AspNetCore.Http;
using RentEase.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Domain.Dto
{
    [ExcludeFromCodeCoverage]
    public class PropertyDto
    {
        public Guid OwnerId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Location Location { get; set; }

        [Required]
        public int Beds { get; set; }

        [Required]
        public int Baths { get; set; }

        [Required]
        public int SquareFeet { get; set; }

        [Required]
        public List<string> Amenities { get; set; }

        [Required]
        public List<Rate> Rates { get; set; }

        [Required]
        public SellerInfo SellerInfo { get; set; }
  
        public List<IFormFile> Images { get; set; }

        public bool IsFeatured { get; set; }
    }
}

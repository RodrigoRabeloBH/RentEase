using RentEase.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace RentEase.Contratcs
{
    [ExcludeFromCodeCoverage]
    public class PropertyUpdated : Entity
    {
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Location Location { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SquareFeet { get; set; }
        public List<string> Amenities { get; set; }
        public List<Rate> Rates { get; set; }
        public SellerInfo SellerInfo { get; set; }
        public List<Image> Images { get; set; }
        public bool IsFeatured { get; set; }
    }
}

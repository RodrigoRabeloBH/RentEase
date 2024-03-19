using System.Diagnostics.CodeAnalysis;

namespace RentEase.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class SellerInfo 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        //EF Relation
        public Guid PropertyId { get; set; }
    }
}

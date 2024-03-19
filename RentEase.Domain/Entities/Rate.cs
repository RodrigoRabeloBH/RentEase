using System.Diagnostics.CodeAnalysis;

namespace RentEase.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Rate
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        //EF Relation
        public Guid PropertyId { get; set; }
    }
}

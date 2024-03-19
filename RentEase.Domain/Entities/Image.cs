using System.Diagnostics.CodeAnalysis;

namespace RentEase.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Image
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public Uri Uri { get; set; }

        //EF Relation
        public Guid PropertyId { get; set; }
    }
}

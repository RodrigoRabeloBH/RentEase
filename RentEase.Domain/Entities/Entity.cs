using System.Diagnostics.CodeAnalysis;

namespace RentEase.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public abstract class Entity
    {
        public Guid Id { get; set; } = new Guid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

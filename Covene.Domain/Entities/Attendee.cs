namespace Covene.Domain.Entities
{
    /// <summary>
    /// The constructor is marked as 'internal' to restrict creation of Attendee objects to within the same assembly.
    /// This enforces that Attendee instances are only created through controlled domain logic (e.g., via factories or aggregates),
    /// preventing misuse from external code.
    /// 
    /// The properties use 'private set' to ensure immutability after construction,
    /// maintaining a consistent and valid state for the Attendee entity as per DDD (Domain-Driven Design) principles.
    /// </summary>

    public class Attendee
    {
        internal Attendee(Invitation invitation) { 
            
            GatheringId = invitation.GatheringId;
            MemberId = invitation.MemberId;
            CreateOnUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// using private set helps maintain a clean and predictable state for your objects, making sure that the data is protected from unwanted modifications after construction.
        /// This is especially useful for value objects or domain entities in domain-driven design (DDD).
        /// </summary>
        public Guid GatheringId { get; private set; }
        public Guid MemberId { get; private set; }
        public DateTime CreateOnUtc { get; private set; }
    }
}

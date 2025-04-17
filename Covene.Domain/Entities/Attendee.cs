namespace Covene.Domain.Entities
{
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

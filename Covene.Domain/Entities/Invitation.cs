namespace Covene.Domain.Entities
{
    public class Invitation
    {

        public Guid Id { get; private set; }
        public Guid GatheringId { get; private set; }
        public Guid MemberId { get; private set; }
        public InvitationStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ModifiedOnUtc { get; private set; }

        internal void Expire()
        {
            Status = InvitationStatus.Expired;
            ModifiedOnUtc = DateTime.UtcNow;
        }

        internal Attendee Accept()
        {

            Status = InvitationStatus.Accepted;
            ModifiedOnUtc = DateTime.UtcNow;
            var attendee = new Attendee(this);
            return attendee;
        }
    }
}

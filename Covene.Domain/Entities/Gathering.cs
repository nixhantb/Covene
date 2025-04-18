
// RICH Domain DDD approach
namespace Covene.Domain.Entities
{
    public class Gathering
    {
        private readonly List<Invitation> _invitations = new();
        private readonly List<Attendee> _attendees = new();

        private Gathering(
            
            Guid id, 
            Member creator,
            GatheringType type,
            DateTime scheduledAtUtc,
            string name,
            string? location)
        {

            Id = id;
            Creator = creator;
            Type = type;
            ScheduledAtUtc = scheduledAtUtc;
            Name = name;
            Location = location;
        }
        public Guid Id { get; private set; }

        public Member Creator { get; private set; }

        public GatheringType Type { get; private set; }

        public string Name { get; private set; }

        public DateTime ScheduledAtUtc { get; private set; }
        public string? Location { get; private set; }
        public int? MaximumNumberOfAttendees { get; private set; }

        public DateTime? InvitationsExpireAtUtc { get; private set; } // must RSVP X hours before

        public int NumberOfAttendees { get; private set; }

        public IReadOnlyCollection<Attendee> Attendees => _attendees;
        public IReadOnlyCollection<Invitation> Invitations => _invitations;

        /// <summary>
        /// Creates a Gathering based on its type:
        /// - WithFixedNumberOfAttendees: Requires a max attendee limit.
        /// - WithExpirationForInvitations: Requires an expiration time (in hours) before the event.
        /// Throws exception if required values are missing for the selected type.
        /// If fixed attendees say 50, no expiry
        /// If no fixed attendees, expire before X hours.
        /// </summary>

        public static Gathering Create(

            Guid id,
            Member creator,
            GatheringType type,
            DateTime scheduledAtUtc,
            string name,
            string? location,
            int? maximumNumberOfAttendees,
            int? invitationsValidBeforeInHours

            )
            // create the gatherings

        {
            var gathering = new Gathering(

                Guid.NewGuid(),
                creator,
                type,
                scheduledAtUtc,
                name,
                location
                );


            // calculate the gathering type details

            switch(gathering.Type )
            {
                // Ex: A conference room can fit only 50 people.
                case GatheringType.WithFixedNumberOfAttendees:
                    if(maximumNumberOfAttendees is null)
                    {
                        throw new Exception(
                            $"{nameof(maximumNumberOfAttendees)} can't be null."
                            );

                    }
                    gathering.MaximumNumberOfAttendees = maximumNumberOfAttendees;
                    break;


                case GatheringType.WithExpirationForInvitations:

                    if(invitationsValidBeforeInHours is null)
                    {
                        throw new Exception($"{nameof(invitationsValidBeforeInHours)} can't be null");
                    }

                    gathering.InvitationsExpireAtUtc = 
                        gathering.ScheduledAtUtc.AddHours(-invitationsValidBeforeInHours.Value); 
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(GatheringType));
            }

            return gathering;

        }

        public Invitation SendInvitation(Member member)
        {
            if(Creator.Id == member.Id)
            {
                throw new Exception("Can't send invitation to the gathering creator");

            }

            var invitation = new Invitation(Guid.NewGuid(), member, this);
            _invitations.Add( invitation );
            return invitation;

        }

        public Attendee? AcceptInvitation(Invitation invitation)
        {
            var expired = (Type == GatheringType.WithFixedNumberOfAttendees

                                    && NumberOfAttendees == MaximumNumberOfAttendees)
                                    || (Type == GatheringType.WithExpirationForInvitations
                                       && InvitationsExpireAtUtc < DateTime.Now);
                                    );
            if (expired)
            {
                invitation.Expire();
                return null;
            }

            var attendee = invitation.Accept();
            _attendees.Add( attendee );
            NumberOfAttendees++;
            return attendee; 
        }
 
    }
}

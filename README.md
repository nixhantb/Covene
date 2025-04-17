

# Covene

Covene is a backend system designed to manage organized gatherings. It focuses on invitation-based participation, allowing members to send, accept, or decline invitations to scheduled events. This system applies Domain-Driven Design (DDD) principles and implements key architectural patterns such as Repository and Unit of Work.

---

## Features

- Members can send and respond to invitations
- Support for various gathering types and statuses
- Attendee tracking for each gathering
- Invitation status management (Pending, Accepted, Declined)
- Role-based responsibilities (Organizer, Attendee)

---

## Domain Model

**Core Entities:**
- `Member`: A user who can send or receive invitations.
- `Covene` (formerly Gathering): An event created by a member.
- `Invitation`: A request for a member to attend a covene.
- `Attendee`: Represents a member's confirmed attendance at a covene.

**Value Objects / Enums:**
- `InvitationStatus`: Enum indicating current state of the invitation.
- `CoveneType`: Enum representing the type of event.

---

## Tech Stack

- .NET 8
- C#
- MediatR
- Entity Framework Core
- Clean Architecture principles

---

## Architecture

This project is structured with a clear separation of concerns:

- `Domain`: Core logic and rules
- `Application`: Use cases and commands/queries
- `Infrastructure`: Data access implementations
- `Web`: API layer (optional, not part of core domain)

---

## Getting Started

1. Clone the repository
2. Configure your database and `appsettings.json`
3. Run migrations using EF Core
4. Start the application

---

## Contributing

Open to ideas and improvements. PRs and issues are welcome.


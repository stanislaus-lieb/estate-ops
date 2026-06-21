# ADR 0001: Web Application With Dedicated Backend API

## Status

Accepted for initial planning.

## Context

EstateOps is a property management application with:

- Multi-organization data isolation.
- User authentication.
- Role-based access control.
- PostgreSQL persistence.
- Document handling.
- Background jobs.
- AI assistant integration with tool execution.
- Future financial workflows.

The frontend will be built with Angular and TypeScript. The database will be PostgreSQL.

The open question was whether the system needs a backend API or whether this abstraction layer is unnecessary.

## Decision

EstateOps will use a dedicated backend API.

The planned backend stack is ASP.NET Core on .NET 10 with C# 14, Entity Framework Core, and the Npgsql PostgreSQL provider.

## Rationale

A backend API is required to protect and coordinate:

- Authentication.
- Organization membership checks.
- Authorization and role permissions.
- Server-side validation.
- Organization-scoped data access.
- Audit logging.
- Document storage and download authorization.
- Encrypted LLM provider settings.
- AI tool execution.
- Background jobs.
- EF Core migrations.
- Future accounting workflows.

Direct database access from the Angular client would make security, organization isolation, API-key protection, and tool execution unsafe and hard to govern.

## Consequences

Positive:

- Stronger security boundary.
- Clear place for business rules.
- Better auditability.
- Cleaner AI tool harness.
- Easier future integrations.
- Better control over background jobs and financial workflows.

Tradeoffs:

- More code and deployment complexity than a frontend-only approach.
- Requires API versioning and contract discipline.
- Requires backend test coverage.

## Follow-Up Decisions

- Authentication session model.
- Role and permission storage model.
- Background job framework.
- Document storage abstraction details.
- AI provider abstraction and conversation retention.

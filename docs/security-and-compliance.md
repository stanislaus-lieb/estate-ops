# Security And Compliance

Security is a core architectural concern because EstateOps stores personal data, lease information, documents, and eventually financial data.

## Authentication

Initial authentication:

- Username or email plus password.
- Secure password hashing.
- Password reset flow.
- Rate limiting.
- Session refresh handling.

Planned authentication extensions:

- Apple OAuth.
- Google OAuth.
- Microsoft OAuth.
- Linking external identities to existing users.

## Session Strategy

Preferred direction:

- Access token or authenticated session for API calls.
- Refresh token or secure session cookie.
- HTTP-only cookies where possible.
- CSRF protection when cookie-based auth is used.
- Short access lifetime and revocable sessions.

The final decision should be captured in a dedicated ADR during implementation.

## Organization Isolation

The UI lets a user switch the current organization through a user menu similar to the referenced Claude-style account menu.

The backend must enforce organization scope on every business request:

- Resolve current user.
- Resolve current organization.
- Verify active membership.
- Verify role and permission.
- Apply organization filter to all business data.

The frontend can store the selected organization for convenience, but the backend must never trust it without membership verification.

## Data Filtering

EF Core should apply organization-aware query filters for organization-owned tables.

Application services should also require `OrganizationId` explicitly in commands and queries. This creates two layers of protection:

- Application-level intent.
- Persistence-level filtering.

PostgreSQL row-level security can be evaluated later for defense in depth, especially if the system grows toward enterprise or hosted SaaS use.

## Roles And Rules

Initial seed roles:

| Role | Intent |
| --- | --- |
| `Owner` | Full control over organization settings, users, billing-relevant settings, and all data. |
| `Administrator` | Manage operational data and invite/manage users, but not necessarily transfer ownership. |
| `PropertyManager` | Manage properties, units, residents, leases, documents, notes, and tasks. |
| `Accountant` | Read operational data and manage future receivables, payments, deposits, and reports. |
| `Viewer` | Read-only access. |

Authorization is based on rules.

EstateOps ships global system role templates and system rule templates. When a new organization is created, these templates are copied into organization-local `Roles` and `Rules`. Users initially cannot customize rule sets, but the local copy keeps the model ready for future organization-specific rule sets.

Object-level restrictions inside one organization are not part of the initial scope.

## Soft Delete

Soft delete is the default for most mutable business records.

Recommended columns:

- `IsDeleted`
- `DeletedAt`
- `DeletedByUserId`

Soft-deleted records are hidden from normal UI and normal API responses.

Restore behavior should be planned per entity. For example, restoring a resident may be allowed, but restoring accounting records might need stronger checks.

## Anonymization

EstateOps needs a way to anonymize personally identifiable data where legally possible.

Anonymization should be modeled as an explicit operation, not as a normal delete.

Examples of anonymizable fields:

- Name.
- Email.
- Phone.
- Current address.
- Personal notes that contain identifying data.

Fields that may need retention:

- Contract dates.
- Financial records.
- Audit events.
- Legal document metadata.

The exact anonymization rules depend on jurisdiction, retention obligations, and product positioning. The architecture should support anonymization jobs and audit them.

## Audit Logs

Audit logs should record important changes:

- Login and security events.
- Organization switch and membership changes.
- Role changes.
- Property, unit, resident, and lease changes.
- Rent term changes.
- Document upload and deletion.
- AI tool executions.
- Anonymization and soft-delete operations.

Audit logs should be append-only from the application perspective.

## Document Security

Documents can contain sensitive personal and contractual information.

Access to documents must be checked through:

- Organization membership.
- Object-level relationship.
- Role/permission.
- Soft-delete state.

When external storage is used, signed URLs or temporary access tokens should be generated server-side. Permanent public links should not be used.

## LLM Data Protection

LLM calls can expose personal data to external providers. The system needs clear organization-level settings and user-facing transparency before production.

Important decisions:

- Which provider is used.
- Whether prompts and responses are stored.
- AI conversations have no automatic expiration by default in the initial product.
- Whether users can archive, delete, or bulk-delete older AI conversations.
- Whether personal data can be sent to external LLM providers.
- Whether self-hosted or local LLM endpoints are supported.

The backend should minimize context and send only data needed for the current request.

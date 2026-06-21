# Domain Model

EstateOps uses English names in source code, API contracts, and database objects.

## Naming Rules

- Entity classes are singular: `User`, `Organization`, `Property`, `Unit`, `Resident`, `Lease`.
- Database tables are plural: `Users`, `Organizations`, `Properties`, `Units`, `Residents`, `Leases`.
- Multi-tenancy uses the term `Organization`, not `Tenant`, because `Tenant` conflicts with the real-estate meaning of "Mieter".
- German UI text should translate domain names for users, for example `Organization` as "Organisation" and `Resident` as "Mieter".

## Core Concepts

`Organization`

Represents a customer workspace, company, owner group, or property-management organization. Users can belong to multiple organizations.

For a new user, the first organization can be named `Personal` by default. The user can later rename it to a company, owner name, or workspace name. Conceptually this behaves like a workspace in tools such as Claude or ChatGPT.

`User`

Represents a login identity. Users are global and can be members of multiple organizations.

`OrganizationMembership`

Connects a user to an organization and defines role assignments.

`Property`

Represents a managed real-estate object, such as an apartment building, house, or commercial property.

`Unit`

Represents a rentable unit inside a property. A property can have many units.

`Resident`

Represents a person or party that can participate in a lease. A resident can be a natural person or a company. The UI exposes this as an `IsCompany` checkbox. Residents are not directly assigned to units for historical correctness. They are connected through leases.

`Lease`

Represents the rental relationship between one unit and one or more residents over time. The lease is the central object for future receivables, deposits, rent terms, notices, and contract state.

A lease has a display name. The system pre-fills this name from assigned residents, but the user can override it. Example: `Familie Mueller`.

`RentTerm`

Represents rent values that are valid from a specific date. This allows planned rent changes without overwriting historical values.

`Document`

Represents a file and its metadata. Documents can be attached to organizations, properties, units, residents, and leases.

`Note`

Represents human-readable notes attached to domain objects.

`Task`

Represents follow-up work, reminders, and operational tasks.

`AuditLogEntry`

Represents a record of security-relevant and business-relevant changes.

`Tag`

Represents an organization-scoped label that can be attached to supported business objects. Tags can be global or restricted to a specific object type.

`Country`

Represents global country reference data. Countries are maintained by EstateOps, not by each organization.

## Organization Scope Rule

All organization-owned business data must include `OrganizationId`.

This applies to properties, units, residents, leases, rent terms, documents, notes, tasks, audit logs, settings, and future accounting tables.

Pure identity tables are global and do not belong to a single organization. Examples:

- `Users`
- `UserCredentials`
- `ExternalLogins`
- `UserSessions`
- `Organizations`

Relationship tables such as `OrganizationMemberships` include `OrganizationId` because they connect users to organizations.

## Initial Tables

### Global Reference Data

| Table | Entity | Organization-scoped | Purpose |
| --- | --- | --- | --- |
| `Countries` | `Country` | No | Global country catalog for addresses. |

### Identity And Organization

| Table | Entity | Organization-scoped | Purpose |
| --- | --- | --- | --- |
| `Users` | `User` | No | Global login user. |
| `UserCredentials` | `UserCredential` | No | Password credential data. |
| `ExternalLogins` | `ExternalLogin` | No | Future OAuth identities. |
| `UserSessions` | `UserSession` | No | Refresh/session tracking. |
| `Organizations` | `Organization` | No | Workspace/customer organization. |
| `OrganizationMemberships` | `OrganizationMembership` | Yes | User membership and status. |
| `SystemRoleTemplates` | `SystemRoleTemplate` | No | Global seed templates for organization roles. |
| `SystemRuleTemplates` | `SystemRuleTemplate` | No | Global seed templates for authorization rules. |
| `Roles` | `Role` | Yes | Organization-local role copy created from seed templates. |
| `Rules` | `Rule` | Yes | Organization-local authorization rule copy created from seed templates. |
| `RoleRules` | `RoleRule` | Yes | Rules granted to roles. |

Initial role and rule storage uses seeded defaults that are copied into each organization at creation time. Custom role/rule sets are planned for a later phase, so organization-local copies are intentional.

Object-level restrictions are not part of the initial scope. Authorization checks are organization-scoped and rule-based.

### Property Management

| Table | Entity | Purpose |
| --- | --- | --- |
| `Properties` | `Property` | Managed property or building. |
| `Units` | `Unit` | Rentable unit belonging to a property. |
| `Residents` | `Resident` | Person or party connected to leases. |
| `Leases` | `Lease` | Rental relationship between unit and resident party. |
| `LeaseResidents` | `LeaseResident` | Many-to-many join between leases and residents. |
| `RentTerms` | `RentTerm` | Time-based rent values for a lease. |
| `UnitRentDefaults` | `UnitRentDefault` | Default or target rent values for a unit, including vacancies. |

Important design choice: residents should not be assigned directly to units. The assignment happens through `Leases`, because unit occupancy changes over time and lease history matters.

A unit can be vacant. In that case there is no active lease, but the unit can still have default rent values. This allows vacancy reporting and later calculations such as lost expected rent.

### Operational Tables

| Table | Entity | Purpose |
| --- | --- | --- |
| `Documents` | `Document` | Metadata, owner object, and template flag for uploaded documents. |
| `DocumentVersions` | `DocumentVersion` | Version metadata, checksum, content type, storage pointer. |
| `DocumentBlobs` | `DocumentBlob` | Default PostgreSQL-backed binary content. |
| `Notes` | `Note` | Notes attached to business objects. |
| `Tasks` | `Task` | Follow-up work and reminders. |
| `Tags` | `Tag` | Organization-scoped tags, optionally restricted to one object type. |
| `TagAssignments` | `TagAssignment` | Links tags to supported objects. |
| `AuditLogEntries` | `AuditLogEntry` | Immutable audit events. |
| `OrganizationSettings` | `OrganizationSetting` | Organization-specific settings. |
| `AiProviderSettings` | `AiProviderSetting` | Encrypted LLM provider settings. |
| `AiConversations` | `AiConversation` | Saved assistant chat sessions. |
| `AiConversationMessages` | `AiConversationMessage` | Messages and tool events within a chat session. |

## Lease And Rent Terms

Leases should carry relationship information:

- `OrganizationId`
- `UnitId`
- `DisplayName`
- `LeaseNumber`
- `StartDate`
- `EndDate`
- `NoticeDate`
- `MoveInDate`
- `MoveOutDate`
- `Status`
- `ShowsVat`
- `VatRatePercent`
- `Notes`

Rent terms should carry money values and validity:

- `LeaseId`
- `ValidFrom`
- `ValidTo`
- `ColdRentAmount`
- `OperatingCostsAmount`
- `AdditionalCostsAmount`
- `Currency`
- `Reason`

This makes planned rent increases natural:

- Current rent term valid from 2026-01-01.
- Future rent term valid from 2026-10-01.
- The UI can show both current and planned values.

## Unit Rent Defaults

Unit rent defaults describe the expected or advertised rent for a unit when there is no active lease.

They should support:

- `UnitId`
- `ValidFrom`
- `ValidTo`
- `DefaultColdRentAmount`
- `DefaultOperatingCostsAmount`
- `DefaultAdditionalCostsAmount`
- `Currency`

When a lease is created from a vacant unit, the UI can pre-fill the first rent term from the current unit rent default.

## Resident Fields

Residents can represent natural persons or companies.

Suggested fields:

- `IsCompany`.
- `DisplayName`.
- `FirstName`.
- `LastName`.
- `CompanyName`.
- `VatIdentificationNumber`.
- `Street`.
- `PostalCode`.
- `City`.
- `CountryId`.
- `Email`.
- `Phone`.
- `Comment`.

UI behavior:

- `IsCompany = false`: show the natural-person fields such as first name and last name.
- `IsCompany = true`: additionally show company name and VAT identification number.
- Address fields should include country from the global `Countries` table.
- `DisplayName` can be derived from person or company fields, but should remain overrideable if needed later.

## Lease VAT

Whether VAT is shown belongs to the lease, not directly to the resident.

Commercial leases may need VAT handling. The exact tax model belongs to a later financial/accounting slice, but the first lease model should leave room for:

- Whether VAT is shown for a lease.
- Which VAT rate applies.
- Whether the resident/company has a VAT identification number.

The first version can use `ShowsVat` and `VatRatePercent` on `Leases`. Future accounting can replace or extend this with tax rules, tax codes, and historical rate tables.

## Documents And Tags

Documents support versions from the beginning.

The latest version is displayed by default, while older versions remain accessible for authorized users.

Document preview is not required for the first release.

Organization-level documents are supported. This is important for templates such as lease templates, letter templates, or standard forms. Template file formats are not restricted in the first version because templates are stored and downloaded, not edited in the browser.

Potential document fields:

- `OwnerObjectType`: `Organization`, `Property`, `Unit`, `Resident`, or `Lease`.
- `OwnerObjectId`.
- `IsTemplate`.
- `TemplateName`.
- `DocumentCategory`.

Future workflow example:

- A user opens a lease.
- The UI offers "Create document from template".
- The user selects an organization-level template.
- EstateOps creates a new lease-level document from that template.

Preferred future template formats are `.docx`, `.xlsx`, and fillable `.pdf`, once document generation, editing, or PDF form tooling is added.

Tags are organization-scoped and can be:

- Global, with `AvailableForObjectType = null`.
- Restricted to an object type such as `Document`, `Note`, `Task`, `Property`, `Unit`, `Resident`, or `Lease`.

For module-specific tag pickers, object-type-specific tags should appear first, sorted alphabetically. Global tags should appear below them, also sorted alphabetically, separated by a visual divider in the UI.

Tags start as text-only labels.

Tag customization can later add:

- `Color`.
- `IconKey`.

`IconKey` should reference a curated application icon catalog, not arbitrary user-provided CSS classes.

Examples of document-specific tags:

- `Mietvertrag`
- `Nebenkosten`
- `Mieterhoehung`
- `Uebergabeprotokoll`

## Future Accounting Extension

Future receivables should be booked against the lease, not directly against a unit or resident.

Likely future tables:

- `Receivables`
- `ReceivableLines`
- `Payments`
- `PaymentAllocations`
- `Deposits`
- `DunningCases`
- `ServiceChargeStatements`

This keeps the first phase focused on master data while leaving the model ready for financial tracking.

## Soft Delete

Most organization-owned tables should support soft delete:

- `IsDeleted`
- `DeletedAt`
- `DeletedByUserId`

Default queries should exclude deleted records.

Some records, such as audit logs and accounting records, should not be deleted or should have stricter deletion rules.

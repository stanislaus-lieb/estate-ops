# Roadmap

This roadmap is a working proposal. It should evolve with product discussions.

## Phase 0: Product And Architecture Foundation

- Establish documentation.
- Confirm domain terminology.
- Confirm technology stack.
- Define initial data model.
- Define organization isolation rules.
- Define AI assistant architecture.
- Capture architecture decisions as ADRs.

## Phase 1: Project Scaffold

- Create Angular application.
- Create ASP.NET Core solution.
- Add PostgreSQL and EF Core.
- Add Docker Compose for local development.
- Add basic CI expectations.
- Add formatting and linting.
- Add initial health check endpoint.
- Add Tailwind CSS and daisyUI.
- Establish the first custom EstateOps UI theme.

## Phase 2: Identity And Organizations

- User registration.
- Username/email and password login.
- Create organization during registration.
- Create the first default organization as `Personal`.
- Organization membership model.
- Organization switcher in the UI.
- Seed organization-local roles and rules from system templates.
- Basic settings area.

## Phase 3: Property Master Data

- Properties.
- Units.
- Residents.
- Leases.
- Rent terms with future validity dates.
- Lease VAT flags for commercial leases.
- Unit rent defaults for vacancies and expected rent.
- Responsive list/detail UI patterns.
- Search and filtering.
- Soft delete support.

## Phase 4: Operational Layer

- Documents for organizations, properties, units, residents, and leases.
- Document versioning.
- Organization-level document templates.
- Download/upload template workflow first.
- Tags and tag assignments.
- Text-only tags first, with color and icon selection later.
- Notes.
- Tasks and reminders.
- Audit log.
- Basic background-job infrastructure.

## Phase 5: AI Assistant Foundation

- AI settings.
- Provider configuration.
- Chat sidebar.
- Saved conversations and conversation selector.
- Archive, delete, and bulk cleanup for conversations.
- Backend AI orchestrator.
- Global tools.
- Page-specific context bundles.
- Tool execution audit.
- Confirmation flow for write tools.

## Phase 6: Financial Extension

- Monthly receivable generation.
- Lease receivables.
- Receivable lines.
- Payments.
- Payment allocation.
- Deposits.
- Dunning.
- Service charge reconciliation.
- Accounting and export workflows.

## Phase 7: Collaboration And Integrations

- Invitation flow.
- OAuth login with Apple, Google, and Microsoft.
- External document storage providers.
- Email notifications.
- Calendar/reminder integrations.
- Advanced reporting.

## Open Product Questions

- Which embedded document editor provider should be evaluated first: ONLYOFFICE Docs, Collabora Online, or Microsoft 365 for the web through WOPI?

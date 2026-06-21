# EstateOps Agent Guide

This file is the persistent entry point for future Codex sessions in this repository.

Keep it concise. Detailed product and architecture decisions live in `docs/`; load only the documents needed for the current task.

## Project Snapshot

EstateOps is a German-first property management web application with an AI-friendly architecture.

Core stack baseline:

- Frontend: Angular and TypeScript.
- Styling: Tailwind CSS with daisyUI.
- Icons: Lucide Angular.
- Backend: ASP.NET Core on .NET 10 with C# 14.
- Persistence: PostgreSQL with Entity Framework Core and Npgsql.
- Deployment: Docker images, Docker Compose for local development.

## Language Rules

- Source code, identifiers, API contracts, database object names, tests, and in-code documentation are English.
- User-facing frontend text is German by default.
- Keep the app ready for later internationalization.

## Domain Terminology

- Use `Organization`, not `Tenant`, for the workspace/company/owner context.
- Use `Resident` for a renter/person/company that participates in a lease.
- Use `Lease` for the rental relationship between a unit and one or more residents.
- Entity class names are singular: `User`, `Organization`, `Property`, `Unit`, `Resident`, `Lease`.
- Database table names are plural: `Users`, `Organizations`, `Properties`, `Units`, `Residents`, `Leases`.
- Organization-owned business tables must include `OrganizationId`.

## Documentation Map

Start with the smallest relevant document:

- `docs/README.md`: documentation index and source baseline.
- `docs/project-vision.md`: product goal, principles, scope.
- `docs/architecture.md`: application architecture, stack, layers, API direction.
- `docs/frontend-ui.md`: CSS framework, component strategy, responsive UI, icons.
- `docs/domain-model.md`: entities, tables, naming rules, lease/resident/document/tag model.
- `docs/document-management.md`: documents, templates, storage, future editor providers.
- `docs/ai-assistant.md`: AI sidebar, provider settings, tool harness, conversations.
- `docs/security-and-compliance.md`: auth, organization isolation, roles/rules, GDPR, audit.
- `docs/roadmap.md`: phases and open product questions.
- `docs/adr/`: accepted architecture decision records.

## Working Rules

- Prefer updating docs before scaffolding code when the task is still a product or architecture decision.
- When decisions become stable, capture them in the relevant doc and add an ADR when the choice materially affects architecture, dependencies, security, data model, or workflow.
- Do not duplicate full docs content here. Link to docs instead.
- Do not introduce Bootstrap; the accepted UI stack is Tailwind CSS plus daisyUI.
- Do not store arbitrary icon CSS classes or raw icon markup for user-configurable tags; use Lucide-backed `IconKey` values.
- Do not implement direct frontend access to PostgreSQL or LLM providers; the backend owns auth, organization isolation, tool execution, and provider secrets.
- Treat document editing in-browser as a future provider integration, not a first-release feature.

## Skills Guidance

Do not create repository skills just to mirror normal project documentation.

Create a skill under `.agents/skills/<skill-name>/SKILL.md` only when there is a repeatable specialized workflow that future sessions should trigger automatically or explicitly, for example:

- generating new ADRs in the EstateOps format,
- designing database migrations from the domain model,
- building Angular CRUD screens with EstateOps UI conventions,
- adding AI assistant tools with the required permission, audit, and confirmation pattern.

If a skill is created, keep `SKILL.md` lean and point to the relevant `docs/` files as references.

## Verification

This repository currently contains planning documentation only. For documentation-only changes, proofread and run targeted `rg` checks for terminology consistency.

Once code exists, prefer the project scripts documented in `README.md` or package/project files. Add those commands here only after they are real and stable.


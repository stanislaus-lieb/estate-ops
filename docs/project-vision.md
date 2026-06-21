# Project Vision

EstateOps is a web-based property management application for owners, property managers, and small to mid-sized real estate operations.

The product should make it easy to create properties, split them into units, manage residents, manage leases, maintain rent terms over time, and quickly see all relevant operational information without navigating through deep menus.

## Core Product Promise

EstateOps helps users answer these questions quickly:

- Which properties and units do I manage?
- Who currently lives in or rents a unit?
- What lease is active, since when, and until when?
- Which rent terms apply now and which changes are already planned?
- Which documents, notes, tasks, and audit events belong to this object?
- What should I do next?

## Experience Principles

- The first screen should be operational, not marketing-oriented.
- Important context should be visible immediately: property, unit, resident, lease status, current rent, planned changes, tasks, and documents.
- Navigation should be shallow. A user should reach properties, units, residents, leases, documents, and tasks in a few clicks.
- The UI must work well on desktop and mobile devices such as Android phones and iPhones.
- The interface should be German-first, with internationalization prepared from the start.
- AI assistance should feel native to the application and understand the current page context.

## AI-Friendly Product Principle

EstateOps is designed for LLM and tool friendliness from the beginning.

The application should expose a controlled tool layer that allows an AI assistant to inspect authorized data, navigate the user through the application, prepare changes, and execute safe actions after permission checks. The AI assistant should receive both global tools and page-specific tools.

Example: When the resident page is open, the assistant can receive resident-specific context and tools such as reading lease history, preparing a contact update, showing related documents, or creating a task for that resident.

## Initial Scope

The initial project phase focuses on master data and the architectural foundation:

- Organizations and users.
- Organization switching in the UI.
- Properties.
- Units.
- Residents.
- Leases.
- Time-based rent terms.
- Notes.
- Tasks.
- Documents.
- Audit log.
- AI assistant shell and tool-harness foundation.

## Deferred But Planned Scope

The following areas are intentionally important but not part of the first implementation slice:

- Monthly receivable generation.
- Rent receivables and payment tracking.
- Deposits.
- Dunning and reminders.
- Service charge reconciliation.
- Accounting exports.
- Advanced reporting.
- Invitation flow.
- OAuth login with Apple, Google, and Microsoft.


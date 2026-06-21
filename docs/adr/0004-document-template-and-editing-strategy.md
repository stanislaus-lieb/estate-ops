# ADR 0004: Document Template And Editing Strategy

## Status

Accepted for initial planning.

## Context

EstateOps needs document management for organizations, properties, units, residents, and leases.

Organization-level documents can act as templates for later workflows such as creating a lease document from a standard lease template.

The question was whether EstateOps should support in-browser editing for document templates, especially `.docx`, `.pdf`, and `.xlsx`.

## Decision

The first release stores, versions, tags, uploads, and downloads documents.

Organization-level templates are supported, but the first release does not require in-browser document preview or editing.

Template file formats are not restricted initially. Preferred future formats are `.docx`, `.xlsx`, and fillable `.pdf`.

EstateOps should not build its own Office document editor.

## Rationale

Browsers do not provide built-in, full-fidelity `.docx` or `.xlsx` editing inside an iframe.

For future in-browser editing, EstateOps should integrate a document editor provider instead of building editing capabilities itself.

Potential providers:

- ONLYOFFICE Docs.
- Collabora Online.
- Microsoft 365 for the web through WOPI, if program and licensing requirements fit.

PDF viewing is easier than Office editing. PDF.js can render PDFs in the browser, but robust form filling, annotation persistence, and editing still require dedicated implementation.

## Consequences

Positive:

- First release remains simpler.
- Users can still manage and reuse templates.
- Document storage and versioning are useful immediately.
- The architecture remains open for a future editor provider.

Tradeoffs:

- Users edit templates outside EstateOps at first.
- "Create document from template" starts as a future workflow, not a first-release editor.
- Future editor integration will require provider-specific security, callbacks, locking, and versioning decisions.

## Follow-Up Decisions

- Choose the first editor provider to evaluate.
- Define placeholder syntax for generated documents.
- Decide whether PDF form filling is a core product workflow.
- Decide whether collaborative editing is needed or single-user editing is enough.


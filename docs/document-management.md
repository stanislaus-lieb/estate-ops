# Document Management

EstateOps supports documents as first-class organization-owned data.

## Initial Scope

The first implementation should focus on:

- Uploading documents.
- Downloading documents.
- Versioning documents.
- Attaching documents to supported objects.
- Tagging documents.
- Storing document metadata in PostgreSQL.
- Storing document bytes in PostgreSQL by default through a replaceable storage provider.
- Supporting organization-level documents.
- Marking organization-level documents as templates.

Document preview and in-browser editing are not required for the first release.

## Attachments

Documents can be attached to:

- `Organization`
- `Property`
- `Unit`
- `Resident`
- `Lease`

Organization-level documents are especially useful for templates, for example:

- Lease templates.
- Letter templates.
- Handover protocol templates.
- Service charge templates.
- Internal forms.

## Template Formats

The first version should not restrict template file formats.

Users can upload any allowed document type and download it later. The uploaded file is treated as a stored template, not as an editable in-browser form.

Preferred future template formats:

- `.docx` for lease and letter templates.
- `.xlsx` for spreadsheet-based calculations or lists.
- `.pdf` for fillable forms if the project adds PDF form tooling.

## Future "Create Document From Template" Flow

Future workflow:

- A user opens a lease.
- The UI offers "Create document from template".
- The user selects an organization-level template.
- EstateOps creates a lease-level document from the template.
- EstateOps can later fill placeholders from lease, resident, unit, property, and organization data.

The first version can simply let users download templates manually.

## In-Browser Editing

Modern browsers do not provide built-in, full-fidelity editing for `.docx` or `.xlsx` inside an iframe.

Practical options for in-browser editing are:

- Integrate an online office suite such as ONLYOFFICE Docs.
- Integrate Collabora Online.
- Integrate Microsoft 365 for the web through WOPI, if the product qualifies and users have suitable Microsoft 365 licensing.
- Keep editing external: download, edit locally, upload a new version.

PDFs are easier to display than Office documents. Browsers can often display PDFs, and PDF.js can render PDFs in the browser. However, robust editing, saving annotations, and filling PDF forms still need deliberate tooling and persistence.

## Recommended Direction

Do not build an Office editor ourselves.

For the first release:

- Store documents.
- Version documents.
- Download documents.
- Tag documents.
- Support organization-level templates.

For a later release:

- Add a `DocumentEditorProvider` abstraction.
- Evaluate ONLYOFFICE Docs and Collabora Online first for self-hostable editing.
- Evaluate Microsoft 365 for the web only if WOPI program and licensing requirements fit the product strategy.
- Add PDF form support only after deciding whether PDF templates are a core workflow.

## Document Metadata

Potential fields:

- `OrganizationId`
- `OwnerObjectType`
- `OwnerObjectId`
- `IsTemplate`
- `TemplateName`
- `DocumentCategory`
- `CurrentVersionId`
- `CreatedAt`
- `CreatedByUserId`
- `UpdatedAt`
- `UpdatedByUserId`
- `IsDeleted`

Potential version fields:

- `DocumentId`
- `VersionNumber`
- `FileName`
- `ContentType`
- `FileExtension`
- `FileSize`
- `Checksum`
- `StorageProvider`
- `StorageKey`
- `CreatedAt`
- `CreatedByUserId`


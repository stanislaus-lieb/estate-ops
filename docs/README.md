# EstateOps Documentation

This folder captures the product and architecture decisions before the implementation starts.

## Document Map

- [Project Vision](./project-vision.md): product goal, users, guiding principles, and scope.
- [Architecture](./architecture.md): application shape, technology baseline, deployment, and backend rationale.
- [Frontend UI](./frontend-ui.md): CSS framework choice, component strategy, theming, and responsive UI principles.
- [Domain Model](./domain-model.md): core concepts, naming rules, initial tables, and future extension points.
- [Document Management](./document-management.md): documents, templates, supported formats, and future editor integration.
- [AI Assistant](./ai-assistant.md): LLM client, tool harness, context strategy, and safety model.
- [Security And Compliance](./security-and-compliance.md): authentication, organization isolation, roles, audit, GDPR, and soft delete.
- [Roadmap](./roadmap.md): proposed phases and explicitly deferred areas.
- [ADRs](./adr/): architecture decision records.

## Current Planning Baseline

Planning date: 2026-06-21.

Primary product language is German. Source code, identifiers, API contracts, database object names, and in-code documentation are English.

The current technology assumption is:

- Angular and TypeScript for the web client.
- Tailwind CSS and daisyUI for frontend styling, with Angular/CDK-backed behavior where needed.
- ASP.NET Core on .NET 10 with C# 14 for the backend API.
- Entity Framework Core with the Npgsql PostgreSQL provider.
- PostgreSQL as the primary database.
- Dockerized deployment.

Version-sensitive choices should be verified again before scaffolding. Current reference links:

- [Angular version compatibility](https://angular.dev/reference/versions)
- [Tailwind CSS documentation](https://tailwindcss.com/docs/installation)
- [daisyUI documentation](https://daisyui.com/docs/install/)
- [Bootstrap documentation](https://getbootstrap.com/docs/5.3/getting-started/introduction/)
- [Lucide Angular documentation](https://lucide.dev/guide/angular)
- [ONLYOFFICE Docs API](https://api.onlyoffice.com/docs/docs-api/get-started/basic-concepts/)
- [Microsoft 365 for the web WOPI integration](https://learn.microsoft.com/en-us/microsoft-365/cloud-storage-partner-program/online/overview)
- [PDF.js](https://mozilla.github.io/pdf.js/)
- [.NET support policy](https://dotnet.microsoft.com/en-us/platform/support/policy/dotnet-core)
- [What's new in C# 14](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-14)
- [Npgsql EF Core provider](https://www.npgsql.org/efcore/)

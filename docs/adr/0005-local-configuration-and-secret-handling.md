# ADR 0005: Local Configuration And Secret Handling

## Status

Accepted for initial scaffold.

## Context

EstateOps needs PostgreSQL configuration during local development and deployment, but database passwords and future provider API keys must not be committed to source control.

The initial development database may live outside the repository and may not be reachable from every developer machine or CI runner.

## Decision

EstateOps stores only non-sensitive defaults and placeholders in tracked files.

Runtime configuration is provided through environment variables, Docker Compose variable substitution, or untracked `.env` files. `.env` and `.env.*` files remain ignored by git, while `.env.example` documents the required keys without secret values.

The backend reads PostgreSQL settings from the `EstateOps:Database` configuration section and also supports direct `ESTATEOPS_DB_*` environment variables for local shells and containers.

## Rationale

This keeps local setup explicit without leaking credentials into GitHub. It also keeps the same configuration path usable for Docker Compose, CI, and later hosted deployments.

## Consequences

Positive:

- No database password is present in source code, appsettings, Dockerfiles, or CI.
- Developers can point the app at a local or external PostgreSQL instance with the same keys.
- CI can validate build and linting without requiring production-like secrets.

Tradeoffs:

- A developer must create a local `.env` file or export environment variables before running database-backed endpoints.
- Readiness checks can fail on machines that cannot reach the configured database.

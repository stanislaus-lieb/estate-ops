# EstateOps

EstateOps is a web-based property management application with a German-first user interface and an AI-friendly architecture.

The project now contains the Phase 1 scaffold. See [docs/README.md](./docs/README.md) for the documentation index.

## Stack

- Frontend: Angular, TypeScript, Tailwind CSS, daisyUI, Lucide Angular.
- Backend: ASP.NET Core on .NET 10 and C# 14.
- Persistence: PostgreSQL with Entity Framework Core and Npgsql.
- Local orchestration: Docker Compose.

## Repository Layout

```text
src/
  EstateOps.Api/
  EstateOps.Application/
  EstateOps.Domain/
  EstateOps.Infrastructure/
  EstateOps.Web/
tests/
  EstateOps.Api.Tests/
docker/
docs/
```

## Prerequisites

- Docker and Docker Compose for the canonical build and test environment.
- Optional for local non-containerized development: .NET 10 SDK.
- Optional for local frontend work: Node.js 24 LTS or Node.js 26 with npm 11 or newer.

Angular 22 does not support odd-numbered Node.js 25 releases.

## Local Configuration

Database secrets are intentionally not committed.

Copy `.env.example` to `.env` and set values for your local database:

```sh
cp .env.example .env
```

For an existing PostgreSQL instance, set the host, port, database, username, and password only in `.env` or your shell environment.

When the API starts, it first checks the configured target database. If that database does not exist, the API connects to `ESTATEOPS_DB_MAINTENANCE_DATABASE` and attempts to create it. If the configured user does not have the required PostgreSQL `CREATEDB` privilege, the API logs a clear error and the readiness endpoint stays unavailable until the database is created manually or the permissions are fixed.

For the backend, the same values can also be exported as environment variables:

```sh
export ESTATEOPS_DB_HOST=localhost
export ESTATEOPS_DB_PORT=5432
export ESTATEOPS_DB_NAME=estateops
export ESTATEOPS_DB_MAINTENANCE_DATABASE=postgres
export ESTATEOPS_DB_USERNAME=estateops
export ESTATEOPS_DB_PASSWORD="<local password>"
export ESTATEOPS_DB_SSL_MODE=Prefer
export ESTATEOPS_DB_GSS_ENCRYPTION_MODE=Disable
```

`.env` and `.env.*` are ignored by git.

## Development Commands

Canonical checks run in Docker:

```sh
npm run check
```

Backend-only Docker checks:

```sh
npm run api:test:docker
```

Frontend-only Docker checks:

```sh
npm run web:check:docker
```

Local frontend development:

```sh
cd src/EstateOps.Web
npm install
npm start
```

Local backend development, when the .NET SDK is installed:

```sh
dotnet restore EstateOps.sln
dotnet run --project src/EstateOps.Api
```

Health endpoints:

- `GET /api/health`
- `GET /api/health/ready`

Docker Compose:

```sh
docker compose up --build
```

Open the UI at `http://localhost:4200`. The web container proxies `/api/*` requests to the API container.

Useful startup diagnostics:

```sh
docker compose logs api
docker compose exec web wget -qO- http://127.0.0.1/api/health/ready
```

To use the optional local PostgreSQL container:

```sh
docker compose --profile local-db up --build
```

## Verification

Preferred:

```sh
npm run check
```

Optional local checks when the SDKs are installed:

```sh
npm --prefix src/EstateOps.Web run format:check
npm --prefix src/EstateOps.Web run lint
npm --prefix src/EstateOps.Web run build
dotnet build EstateOps.sln
dotnet test EstateOps.sln
```

## License

EstateOps is licensed under the GNU Affero General Public License v3.0 or later (`AGPL-3.0-or-later`). See [LICENSE](./LICENSE).

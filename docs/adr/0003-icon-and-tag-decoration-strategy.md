# ADR 0003: Icon And Tag Decoration Strategy

## Status

Accepted for initial planning.

## Context

EstateOps needs icons for application actions and may later allow users to decorate tags with colors and icons.

The user-facing tag system should start simple as text-only labels. Later, organization users should be able to manage tag colors and assign an icon from a curated selection.

The question was whether to use an icon font such as Font Awesome or Material Symbols.

## Decision

Use a curated icon catalog.

For application icons and configurable tag icons, use Lucide Angular.

For tag icons, store an `IconKey` that references the curated catalog. Do not store arbitrary CSS classes, raw HTML, SVG markup, or external URLs in tag records.

Tags start as text-only labels. Tag color and icon assignment are deferred to a later tag-management UI.

## Rationale

Lucide Angular provides a typed Angular integration, standalone component usage, and tree-shakable icons. This fits the Angular application architecture better than globally loading an icon font by default.

An `IconKey` keeps tag decoration safe and portable. It also lets the application switch the underlying icon implementation later without changing tag records.

Font Awesome and Google Material Symbols remain valid alternatives if the team later decides that an icon-font workflow is preferable, but they are not the planning default.

## Consequences

Positive:

- Safer tag customization.
- No arbitrary user-provided icon markup.
- Better bundle control for application icons.
- Easy to render the same tag consistently across the app.

Tradeoffs:

- Users can only choose from approved icons.
- The app needs a small icon registry for configurable tag icons.

# Frontend UI

EstateOps should use a utility-first styling foundation with a small component-class layer.

## Decision

Use Tailwind CSS with daisyUI as the initial frontend styling stack.

Use Angular and Angular CDK-style patterns for behavior that needs strong accessibility and state handling, such as:

- Menus.
- Dialogs.
- Overlays.
- Tooltips.
- Comboboxes.
- Keyboard navigation.
- Focus management.

Do not use Bootstrap as the primary CSS framework.

## Rationale

EstateOps needs a dense operational interface, custom responsive layouts, a persistent AI sidebar, German-first text, and a design direction that should not look like a generic admin template.

Tailwind CSS provides low-level layout and styling control. daisyUI provides semantic component classes, themes, and fast prototyping on top of Tailwind CSS. This combination gives us more control over the final product language than Bootstrap while still avoiding hand-written CSS for every button, input, badge, table, and alert.

Bootstrap remains a strong general-purpose toolkit, but its visual defaults and JavaScript component model are less aligned with a modern Angular application that will have custom shell behavior and a first-class AI assistant.

## Source Baseline

Planning date: 2026-06-21.

Verified source baselines:

- Bootstrap official docs show Bootstrap v5.3.x as the current v5 line.
- daisyUI official docs show daisyUI v5.x and installation as a Tailwind CSS plugin.
- Tailwind CSS official docs show Tailwind CSS v4.x and zero-runtime utility generation.
- Lucide official docs provide an Angular package with tree-shakable standalone icons.

These versions should be verified again during scaffolding.

## Design Direction

EstateOps should feel like a quiet, focused operations tool:

- Dense but readable layouts.
- Clear hierarchy.
- Fast scanning.
- Minimal visual noise.
- No marketing-style landing page as the app entry.
- Mobile layouts that keep primary actions reachable.
- German UI text through translation keys.

The default daisyUI theme should not be used as the final product identity. EstateOps should define a custom theme with restrained colors, clear focus states, and accessible contrast.

## Component Strategy

Use three layers:

1. App-specific Angular components for behavior and state.
2. Tailwind CSS utilities for layout and exact spacing.
3. daisyUI classes for consistent base styling where they fit.

Examples:

- `EstateButtonComponent` can wrap command styling and loading states.
- `EstateDataTableComponent` can standardize dense list behavior.
- `EstateObjectHeaderComponent` can show property, unit, resident, or lease summaries consistently.
- `EstateAiSidebarComponent` owns assistant layout and conversation state.

The project should avoid mixing multiple full CSS frameworks. Bootstrap and daisyUI should not both be used.

## Icons

Use a curated icon strategy instead of arbitrary icon CSS classes.

Recommended direction:

- Use Lucide Angular for application action icons and configurable tag icons.
- Store tag icons as an `IconKey`, not as raw HTML or CSS.
- Render `IconKey` through an approved application icon catalog.
- Keep tags text-only in the first release.
- Add tag color and icon selection later in tag management.

Font Awesome and Google Material Symbols are valid icon-font options, but they should not be introduced by default unless the team explicitly prefers an icon-font workflow. For Angular, Lucide's component package gives better tree-shaking and a cleaner typed integration.

## Responsive Layout Principles

Desktop:

- Persistent left navigation.
- Optional persistent AI sidebar.
- Main content optimized for list/detail workflows.
- Details should use compact sections instead of oversized cards.

Mobile:

- Navigation collapses into a bottom or drawer pattern.
- AI assistant opens as a sheet or full-screen panel.
- List/detail pages should avoid tiny tables and instead use scannable rows.
- Primary actions remain reachable without excessive scrolling.

## Accessibility

CSS classes do not replace accessible component behavior.

Interactive components must support:

- Keyboard navigation.
- Visible focus states.
- Correct ARIA semantics where needed.
- Proper labels and descriptions.
- Screen-reader-friendly validation messages.

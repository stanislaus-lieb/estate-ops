# ADR 0002: Frontend Styling Stack

## Status

Accepted for initial planning.

## Context

EstateOps needs a responsive Angular frontend with:

- Dense operational screens.
- German-first UI text.
- Property, unit, resident, lease, document, note, and task workflows.
- A persistent AI assistant sidebar on desktop.
- A mobile-friendly assistant panel.
- A custom visual identity that should not feel like a generic admin template.

The main options discussed were Bootstrap and daisyUI.

## Decision

Use Tailwind CSS with daisyUI for the initial styling stack.

Use Angular components and Angular CDK-style patterns for behavior-heavy components such as menus, dialogs, overlays, comboboxes, tooltips, focus management, and keyboard navigation.

Do not use Bootstrap as the primary CSS framework.

## Rationale

Tailwind CSS gives precise layout and responsive control. daisyUI adds semantic component classes and theme support on top of Tailwind CSS. This combination fits a custom product shell and AI-first application better than Bootstrap defaults.

Bootstrap is mature and productive, but its visual defaults and JavaScript component model are less aligned with the desired Angular application architecture.

The decision also leaves room for EstateOps-specific components that wrap styling and behavior in a controlled way.

## Consequences

Positive:

- High layout flexibility.
- Fast prototyping with daisyUI components.
- Strong theming foundation.
- Easier to build a custom EstateOps identity.
- Avoids depending on Bootstrap JavaScript behavior.

Tradeoffs:

- Requires design discipline to avoid inconsistent utility-class usage.
- daisyUI component classes do not replace accessible Angular behavior.
- The team must define app-specific component wrappers for repeated patterns.

## Follow-Up Decisions

- Define the first EstateOps daisyUI theme.
- Decide whether to add Angular CDK explicitly during frontend scaffolding.
- Define base components for buttons, inputs, object headers, data tables, badges, dialogs, and the AI sidebar.


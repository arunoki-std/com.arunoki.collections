# com.arunoki.collections — Agent Instructions

Standalone UPM package (own git repo), embedded as a submodule in the Hillybombs project. You are working INSIDE this package: commits go to this repo; the host project updates its submodule pointer separately.

## Package

Allocation-free collection utilities for Unity. Leaf package — depends on nothing.

- Code: `Runtime/` (asmdef `Arunoki.Collections`, no references)
- Key types: `Container<T>`, `SetsCollection`, `SetsTypeCollection<T>`, custom enumerators
- Consumers (in host project): com.arunoki.flow, com.arunoki.core, game code

## ⚠ Status: future under review

Host spec RF-007 (host `specs/refactoring/`): usage audit showed this package serves only `flow`; the default option is to merge it into `com.arunoki.flow` and retire this repo. Until that decision: bugfixes only — no new features, tests, or refactoring here.

## Rules

- NEVER add references to other Arunoki packages or UnityEngine-heavy APIs — this package must stay a dependency-free leaf.
- Public API changes are breaking for all consumers: additive changes preferred; removals/renames need a spec and a version bump.
- Performance is the point: no LINQ in hot paths, no allocations in enumerators. `ReflectionUtils.PropsCache` is a known exception under review.
- Never edit or delete `.meta` files by hand; never invent GUIDs.
- Code style: standard Microsoft C# per `.editorconfig` (4-space, Allman, `Foo()`); run formatter on changed files.
- Specs for this package live in `Specs~/` (the `~` keeps Unity from importing them). Cross-cutting roadmap lives in the host project (`docs/MIGRATION_PLAN.md`).
- Shared skills are available via symlinks in `.agents/skills/` (targets live in the host repo; they dangle in a standalone clone — harmless). Module-specific skills get real folders here, named with the package prefix.

## Verification

The package alone does not compile — it needs a Unity host. Ask the user to compile/run EditMode tests in the Hillybombs project. Tests (when added per RF-004) live in `Tests/` with asmdef `Arunoki.Collections.Tests`.

# Contributing

Thank you for contributing to XamlDS.ITK. This document contains a short set of developer guidelines focused on consistent line endings (EOL) and local Git/editor settings to avoid platform-specific issues.

## Local developer setup

This repository enforces consistent line endings (EOL) across operating systems. Please apply the recommended settings below to align your local environment and prevent noisy diffs or build/script failures.

### Recommended Git settings
- Windows (recommended):
  ```powershell
  git config --global core.autocrlf true
  ```
  This converts LF to CRLF on checkout for better compatibility with Windows editors, and converts CRLF back to LF on commit.

- macOS / Linux (recommended):
  ```bash
  git config --global core.autocrlf input
  ```
  This converts CRLF to LF on commit and leaves LF unchanged on checkout.

### Project-level rules
- `.editorconfig` and `.gitattributes` are provided at the repository root to enforce indentation and EOL rules at editor and Git levels.
- Markdown files may intentionally include trailing spaces (two spaces for hard line breaks). The repository sets `trim_trailing_whitespace = false` for `*.md` in `.editorconfig` to preserve that behavior.

### Repository normalization (if needed)
If you have EOL inconsistencies locally (or after `.gitattributes` changes), normalize working files with:

```powershell
git add --renormalize .
git commit -m "Normalize line endings"
```

Notes:
- Review `git status` and `git diff --staged` before committing normalization changes — normalization may touch many files.
- If you prefer not to change your working files, coordinate with the maintainers before running normalization on a shared branch.

## Quick checklist before committing
- Run `git status` and `git diff` to verify only intended changes are staged.
- Keep changes small and focused; large normalization commits should be reviewed separately.
- After pushing, verify CI builds pass.

If you'd like, we can expand these guidelines with examples for common editors (VS Code, Visual Studio) or add a troubleshooting section. Open an issue or submit a PR with suggestions.

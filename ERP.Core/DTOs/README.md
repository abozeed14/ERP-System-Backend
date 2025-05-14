# DTOs Organization

This directory contains Data Transfer Objects (DTOs) organized by controller/feature.

## Structure

DTOs are organized in subfolders based on the controller or feature they belong to:

- `AuthModels/` - DTOs for authentication and user management
  - Contains models for registration, login, token refresh, and password management

## Adding New DTOs

When adding new DTOs:

1. Create a subfolder for the controller/feature if it doesn't exist
2. Place related DTOs in the appropriate subfolder
3. Use meaningful names that reflect the purpose of the DTO
4. Group related DTOs in a single file when appropriate

## Naming Conventions

- Use descriptive names ending with a suffix that indicates the purpose (e.g., `Model`, `DTO`, `Request`, `Response`)
- For request/response pairs, consider using `{Feature}{Action}Request` and `{Feature}{Action}Response`

## Best Practices

- Keep DTOs focused on their specific use case
- Include data annotations for validation
- Don't expose entity properties that shouldn't be visible to clients
- Consider using AutoMapper for mapping between entities and DTOs
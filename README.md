# UsersApi
Simple Identity API to manage users

Uses MySQL.

This API uses secrets generated locally, since it's not meant for a production environment and are stored in %AppData%\Microsoft\UserSecrets according to https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows

To facilitate local secret generation, the file secrets.json are included in the project root.


using System;
using Esclean.Models.Auth;

namespace Esclean.Services.Session;

public class SessionService : ISessionService
{
    public UserSession? CurrentUser { get; private set; }

    public bool IsAuthenticated =>
        CurrentUser is not null &&
        CurrentUser.IsAuthenticated;

    public void StartSession(UserSession user)
    {
        ArgumentNullException.ThrowIfNull(user);

        if (!user.IsAuthenticated)
        {
            throw new InvalidOperationException(
                "No es posible iniciar sesión con un usuario no autenticado.");
        }

        CurrentUser = user;
    }

    public void EndSession()
    {
        CurrentUser = null;
    }
}
using Esclean.Models.Auth;

namespace Esclean.Services.Session;

public interface ISessionService
{
    UserSession? CurrentUser { get; }

    bool IsAuthenticated { get; }

    void StartSession(UserSession user);

    void EndSession();
}
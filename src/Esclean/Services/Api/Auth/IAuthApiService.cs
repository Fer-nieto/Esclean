using System.Threading;
using System.Threading.Tasks;
using Esclean.Models.Auth;

namespace Esclean.Services.Api.Auth;

public interface IAuthApiService
{
    Task<UserSession?> LoginAsync(
        string employeeNumber,
        string password,
        CancellationToken cancellationToken = default);
}
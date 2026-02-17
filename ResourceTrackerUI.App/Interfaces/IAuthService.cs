

using ResourceTrackerUI.Domain.Models;

namespace ResourceTrackerUI.App.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> LoginAsync(LoginRequestModel loginRequest);
        Task<int> RegisterAsync(RegisterRequestModel registerRequest);
    }
}

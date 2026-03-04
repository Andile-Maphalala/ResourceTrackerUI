using ResourceTrackerUI.Domain.Models;


namespace ResourceTrackerUI.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> LoginAsync(LoginRequestModel loginRequest);
        Task<int> RegisterAsync(RegisterRequestModel registerRequest);
    }
}

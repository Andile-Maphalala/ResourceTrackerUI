

using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.App.Interfaces;
using ResourceTrackerUI.Domain.Models;

namespace ResourceTrackerUI.App.Services
{
    public class AuthService(ResourceTrackerApiClient api) : IAuthService
    {
        public async Task<AuthResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            var dto = new LoginRequest
            {
                Email = loginRequest.Username,
                Password = loginRequest.Password
            };

            var response = await api.ApiAccountLoginAsync(dto);
            var model = new AuthResponseModel
            {
                Id = response.Id,
                UserName = response.UserName,
                Email = response.Email,
                Token = response.Token
            };
            return model;
        }

        public async Task<int> RegisterAsync(RegisterRequestModel registerRequest)
        {
            var dto = new RegisterRequest
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                UserName = registerRequest.UserName,
                Password = registerRequest.Password
            };

            var response = await api.ApiAccountRegisterAsync(dto);
            return response.UserId;
        }
    }
}

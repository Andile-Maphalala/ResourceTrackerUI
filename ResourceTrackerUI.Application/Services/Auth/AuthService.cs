using ResourceTrackerUI.ApiClient;
using ResourceTrackerUI.Application.Interfaces;
using ResourceTrackerUI.Domain.Models;

namespace ResourceTrackerUI.Application.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly ResourceTrackerApiClient _api;

        public AuthService(ResourceTrackerApiClient api)
        {
            _api = api;
        }

        public async Task<AuthResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            var dto = new LoginRequest
            {
                Email = loginRequest.Username,
                Password = loginRequest.Password
            };

            var response = await _api.ApiAccountLoginAsync(dto);
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

            var response = await _api.ApiAccountRegisterAsync(dto);
            return response.UserId;
        }
    }
}

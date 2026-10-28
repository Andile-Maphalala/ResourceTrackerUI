using System.Net;
using Microsoft.AspNetCore.Components;
using ResourceTrackerUI.Application.Interfaces;

namespace ResourceTrackerUI.Application.Services.Auth
{
    /// <summary>
    /// Turns an expired/invalid session (a 401 from any non-auth endpoint) into a redirect to the
    /// login page, instead of leaving the user on a page that keeps failing with 401s.
    /// </summary>
    public class UnauthorizedRedirectHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _storage;
        private readonly NavigationManager _navigation;
        private bool _isRedirecting;

        public UnauthorizedRedirectHandler(ILocalStorageService storage, NavigationManager navigation)
        {
            _storage = storage;
            _navigation = navigation;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized && !IsAuthRequest(request))
            {
                await RedirectToLoginAsync();
            }

            return response;
        }

        private async Task RedirectToLoginAsync()
        {
            // Never bounce the login page itself around.
            if (_isRedirecting)
            {
                return;
            }

            var relativePath = _navigation.ToBaseRelativePath(_navigation.Uri);
            if (relativePath.StartsWith("login", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            // Without a token the request was anonymous; the router already handles those.
            var token = await _storage.GetItemAsync<string>("token");
            if (string.IsNullOrEmpty(token))
            {
                return;
            }

            _isRedirecting = true;

            await _storage.RemoveItemAsync("token");

            var returnUrl = "/" + relativePath;
            _navigation.NavigateTo(
                $"/login?expired=true&returnUrl={Uri.EscapeDataString(returnUrl)}",
                forceLoad: true);
        }

        private static bool IsAuthRequest(HttpRequestMessage request)
        {
            var uri = request.RequestUri;
            if (uri is null)
            {
                return false;
            }

            var path = uri.IsAbsoluteUri ? uri.AbsolutePath : uri.ToString();

            return path.Contains("/api/Account/login", StringComparison.OrdinalIgnoreCase)
                || path.Contains("/api/Account/register", StringComparison.OrdinalIgnoreCase);
        }
    }
}

using ResourceTrackerUI.ApiClient;
using System.Text.Json;

namespace ResourceTrackerUI.Application.Common
{
    public static class ApiExceptionHelper
    {
        private class ApiErrorBody
        {
            public int? StatusCode { get; set; }
            public string? Message { get; set; }
            public string? Detail { get; set; }
            public string? Title { get; set; }
        }

        /// <summary>
        /// Pulls the human-readable message out of an API error response. The API's exception
        /// middleware returns { StatusCode, Message, Errors } for bad requests and not founds, so
        /// that shape is tried before falling back to the generic status-code message.
        /// </summary>
        public static string GetMessage(ApiException ex)
        {
            if (!string.IsNullOrWhiteSpace(ex.Response))
            {
                try
                {
                    var body = JsonSerializer.Deserialize<ApiErrorBody>(
                        ex.Response,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (!string.IsNullOrWhiteSpace(body?.Message))
                        return body.Message;

                    if (!string.IsNullOrWhiteSpace(body?.Detail))
                        return body.Detail;

                    if (!string.IsNullOrWhiteSpace(body?.Title))
                        return body.Title;
                }
                catch (JsonException)
                {
                    // Not JSON - fall through to the generic message.
                }
            }

            return $"The request failed ({ex.StatusCode}).";
        }
    }
}

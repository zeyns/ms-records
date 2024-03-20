using System.Net.Http.Headers;

namespace Records.API.Middlewares;
public class AuthMiddleware(RequestDelegate next, ILogger<AuthMiddleware> logger, IConfiguration configuration) 
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<AuthMiddleware> _logger = logger;
    private readonly HttpClient _httpClient = new ();
    private readonly IConfiguration _configuration = configuration;

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out Microsoft.Extensions.Primitives.StringValues authHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Token not found");
            return;
        }

        string token = authHeader.ToString().Split(' ')[1];

        try
        {
            string urlAuthService = _configuration.GetValue<string>("UrlAuthService") ?? "https://localhost:44318/";
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.PostAsync($"{urlAuthService}Authentication/verify-token", null);
            if (!response.IsSuccessStatusCode)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Invalid or expired token");
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error in token validation: {ex.Message}");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Error in token validation");
            return;
        }
        await _next(context);
    }
}
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post)
        {
            context.Request.EnableBuffering();
            
            var body = await context.Request.ReadFromJsonAsync<object>();

            if (body != null)
            {
                await ValidateAndRespondAsync(context, body);
            }

            context.Request.Body.Position = 0;
        }

        await _next(context);
    }

    private async Task ValidateAndRespondAsync<T>(HttpContext context, T dto)
    {
        var validator = context.RequestServices.GetService<IValidator<T>>();

        if (validator != null)
        {
            ValidationResult result = await validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new { Errors = errors };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
    }
}

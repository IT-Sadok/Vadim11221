using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;

namespace PrivateHospitals.API.Middleware;

public class ValidationMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            context.Request.EnableBuffering();

            try
            {
                var body = await context.Request.ReadFromJsonAsync<object>();
                if (body != null)
                {
                    await ValidateAndRespondAsync(context, body);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new { Errors = new[] { "Internal server error occurred.", ex.Message } };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                return;
            }
            finally
            {
                context.Request.Body.Position = 0; // Reset position after processing
            }
        }

        await _next(context);
    }

    private async Task ValidateAndRespondAsync(HttpContext context, object dto)
    {
        Console.WriteLine($"Validating DTO of type {dto.GetType().Name}");

        var validatorType = typeof(IValidator<>).MakeGenericType(dto.GetType());
        var validator = context.RequestServices.GetService(validatorType) as IValidator;

        if (validator != null)
        {
            var result = await validator.ValidateAsync(new ValidationContext<object>(dto));

            if (!result.IsValid)
            {
                Console.WriteLine($"Validation failed: {string.Join(", ", result.Errors.Select(e => e.ErrorMessage))}");
                var errors = result.Errors.Select(f => f.ErrorMessage).ToList();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new { Errors = errors };
                await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
            }
        }
        else
        {
            Console.WriteLine($"No validator found for type {dto.GetType().Name}");
        }
    }
}

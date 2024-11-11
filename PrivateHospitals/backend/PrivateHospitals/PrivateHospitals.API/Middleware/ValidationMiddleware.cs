using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using PrivateHospitals.Application.Dtos.Doctor;
using PrivateHospitals.Application.Dtos.Patient;
using PrivateHospitals.Application.Dtos.User;

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

            if (body is RegisterDoctorDto doctorDto)
            {
                await ValidateAndRespondAsync(context, doctorDto);
                return;
            }
            else if (body is RegisterPatientDto patientDto)
            {
                await ValidateAndRespondAsync(context, patientDto);
                return;
            }
            else if (body is LoginDto loginDto)
            {
                await ValidateAndRespondAsync(context, loginDto);
                return;
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


using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PrivateHospital.Migration;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospitals.Infrastructure.Data;
using System.Security.Cryptography.Xml;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        var serviceProvider = ConfigureServices();

        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

        logger.LogInformation("Starting the migration process...");

        try
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var context = serviceProvider.GetRequiredService<HospitalDbContext>();

            logger.LogInformation("Testing database connection...");

            if(await context.Database.CanConnectAsync())
            {
                logger.LogInformation("Successfully conected to the database");
            }
            else
            {
                logger.LogWarning("Cannot connect to the database");
                return;
            }

            var doctorRepository = serviceProvider.GetRequiredService<DoctorRepository>();
            var patientRepository = serviceProvider.GetRequiredService<PatientRepository>();
            var appointmentRepository = serviceProvider.GetRequiredService<AppointmentsRepository>();

            var unitOfWork = new UnitOfWork(context, doctorRepository, patientRepository, appointmentRepository);

            Console.WriteLine("Write a JSON file:");
            var filePath = Console.ReadLine();

            logger.LogInformation("Reading JSON file...");

            var migrationData = await ReadJsonAsync(filePath);

                using var transaction = await unitOfWork.BeginTransactionAsync();
                try
                {
                    foreach(var doctor in migrationData.Doctors)
                    {
                        await unitOfWork.DoctorService.SaveDoctor(doctor);
                    }

                    foreach(var patient in migrationData.Patients)
                    {
                        await unitOfWork.PatientService.SavePatientAsync(patient);
                    }

                    foreach (var appointment in migrationData.Appointments)
                    {
                        await unitOfWork.AppointmentService.SaveAppointmentAsync(appointment);
                    }

                    await transaction.CommitAsync();

                    logger.LogInformation("Migration completed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogInformation($"Migration failed: {ex.Message}");
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static async Task<MigrationData>ReadJsonAsync(string filePath)
    {
        using var stream = File.OpenRead(filePath);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return await JsonSerializer.DeserializeAsync<MigrationData>(stream, options) 
            ?? throw new InvalidOperationException("Invalid JSON format.");
    }

    private static ServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("consolesettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        services.AddDbContext<HospitalDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddScoped<DoctorRepository>();
        services.AddScoped<PatientRepository>();
        services.AddScoped<AppointmentsRepository>();

        services.AddLogging(configure => configure.AddConsole());

        return services.BuildServiceProvider();
    }
}
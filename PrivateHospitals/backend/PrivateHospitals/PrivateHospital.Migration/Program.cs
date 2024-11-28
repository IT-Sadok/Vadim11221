
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PrivateHospital.Migration;
using PrivateHospital.Migration.Dto.Repositories;
using PrivateHospitals.Infrastructure.Data;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        Console.WriteLine("Enter the name of the JSON file (with extension, e.g., data.json):");
        string? filePath= Console.ReadLine();

        if(!File.Exists(filePath))
        {
            Console.WriteLine("File not found");
            return;
        }

        try
        {
            string configFile = "consolesettings.json";
            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException($"File {configFile} not found");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(configFile, optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string not found");
            }

            Console.WriteLine("Configuration loaded successfully");

            var options = new DbContextOptionsBuilder<HospitalDbContext>()
                .UseNpgsql(connectionString)
                .Options;

            using var context = new HospitalDbContext(options);

            Console.WriteLine("Testing database connectionString...");
            if (await context.Database.CanConnectAsync())
            {
                Console.WriteLine("Successfully connected to the database");
            }
            else
            {
                throw new Exception("Cannot connect to the database");
            }

            var doctorRepository = new DoctorRepository(context);
            var patientRepository = new PatientRepository(context);
            var appointmentRepository = new AppointmentsRepository(context);

            var unitOfWork = new UnitOfWork(context, doctorRepository, patientRepository, appointmentRepository);

            Console.WriteLine("Reading JSON file...");
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

                await unitOfWork.SaveChangeAsync();
                await transaction.CommitAsync();

                Console.WriteLine("Migration completed successfully.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Migration failed: {ex.Message}");
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
}
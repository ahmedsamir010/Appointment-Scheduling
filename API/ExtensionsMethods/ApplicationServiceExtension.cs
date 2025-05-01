using Infrastructre.Services;

namespace API.ExtensionsMethods;
public static class ApplicationServiceExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {

        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton<IMapper>(new Mapper(mappingConfig));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddApiBehaviorOptions();
        return services;
    }

    private static void AddApiBehaviorOptions(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                var errorResponse = new ApiValidationErrorResponse { Errors = errors };
                return new BadRequestObjectResult(errorResponse);
            };
        });
    }
}

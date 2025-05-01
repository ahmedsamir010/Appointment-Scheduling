namespace API.Mapping;
public class MappingConfigurations : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Appointment, AppointmentResponse>().TwoWays();
        config.NewConfig<Appointment, CreateAppointmentRequest>().TwoWays();
        config.NewConfig<Appointment, UpdateAppointmentRequest>().TwoWays();
    }
}
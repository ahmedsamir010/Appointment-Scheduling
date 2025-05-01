using Domain.Enums;

namespace Domain.Entities;
public class Appointment
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = default!;
    public DateTime DateTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Scheduled;
    public string? Notes { get; set; }
}
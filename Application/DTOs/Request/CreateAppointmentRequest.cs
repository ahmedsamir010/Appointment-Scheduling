using Domain.Enums;

namespace Application.DTOs.Request;
public record CreateAppointmentRequest(
    string CustomerName,
    DateTime DateTime,
    AppointmentStatus Status,
    string? Notes
);

using Domain.Enums;

namespace Application.DTOs.Request;
public record UpdateAppointmentRequest(
    int Id,
    string CustomerName,
    DateTime DateTime,
    AppointmentStatus Status,
    string? Notes
);
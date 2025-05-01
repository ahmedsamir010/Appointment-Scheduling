using Domain.Enums;

namespace Application.DTOs.Response;
public record AppointmentResponse(
    int Id,
    string CustomerName,
    DateTime DateTime,
    AppointmentStatus Status,
    string? Notes
);
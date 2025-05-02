using System.ComponentModel.DataAnnotations;
using Application.CustomAttribute;
using Domain.Enums;

namespace Application.DTOs.Request
{
    public class CreateAppointmentRequest
    {
        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; } = default!;

        [Required(ErrorMessage = "Date and time are required.")]
         [FutureDate(ErrorMessage = "Appointment date and time must be in the future.")]
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = "Invalid appointment status.")]
        public AppointmentStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}
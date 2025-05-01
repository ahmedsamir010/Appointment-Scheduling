using System.ComponentModel.DataAnnotations;
using Application.CustomAttribute;
using Domain.Enums;

namespace Application.DTOs.Request
{
    public class UpdateAppointmentRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        public string CustomerName { get; set; } = default!;

        [Required(ErrorMessage = "Date and time are required.")]
        [FutureDate(ErrorMessage = "Appointment date and time must be in the future.")]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [EnumDataType(typeof(AppointmentStatus), ErrorMessage = "Invalid appointment status.")]
        [ScheduledOnly(ErrorMessage = "Only Scheduled appointments can be updated.")]
        public AppointmentStatus Status { get; set; }

        public string? Notes { get; set; }
    }
}

using Application.DTOs.Request;
using Application.Repositories;
using Domain.Entities;

namespace Application.Services;

public interface IAppointmentService : IBaseService<Appointment>
{
    Task<bool> IsDuplicateAsync(string customerName, DateTime dateTime);
    Task<bool> CreateAppointmentAsync(CreateAppointmentRequest createAppointmentRequest);
    Task<bool> UpdateAppointmentAsync(UpdateAppointmentRequest updateAppointmentRequest);
}
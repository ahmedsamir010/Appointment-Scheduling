using Application.DTOs.Request;
using Application.ResultPattern;
using Domain.Entities;
namespace Application.Services;
public interface IAppointmentService : IBaseService<Appointment>
{
    Task<bool> IsDuplicateAsync(string customerName, DateTime dateTime,int?Id=null);
    Task<Result> CreateAppointmentAsync(CreateAppointmentRequest createAppointmentRequest);
    Task<Result> UpdateAppointmentAsync(int Id, UpdateAppointmentRequest updateAppointmentRequest);
}
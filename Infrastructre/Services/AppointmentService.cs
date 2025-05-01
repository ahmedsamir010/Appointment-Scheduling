using Application.DTOs.Request;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Infrastructre.Implementations;
using Mapster;
namespace Infrastructre.Services;

public class AppointmentService(IUnitOfWork unitOfWork) : BaseService<Appointment>(unitOfWork),IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> CreateAppointmentAsync(CreateAppointmentRequest dto)
    {
        if (dto.DateTime < DateTime.Now)
            return false;

        if (await IsDuplicateAsync(dto.CustomerName, dto.DateTime))
            return false;

        var appointment = dto.Adapt<Appointment>();
        appointment.Status = AppointmentStatus.Scheduled;

        await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> UpdateAppointmentAsync(UpdateAppointmentRequest dto)
    {
        var repo = _unitOfWork.Repository<Appointment>();
        var existing = await repo.GetByIdAsync(dto.Id);

        if (existing == null || existing.Status != AppointmentStatus.Scheduled)
            return false;

        if (dto.DateTime < DateTime.Now)
            return false;

        if (await IsDuplicateAsync(dto.CustomerName, dto.DateTime) &&
            (existing.CustomerName != dto.CustomerName || existing.DateTime != dto.DateTime))
            return false;

        dto.Adapt(existing);  
        await repo.UpdateAsync(existing);
        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> IsDuplicateAsync(string customerName, DateTime dateTime)
    {
        var appointments = await _unitOfWork.Repository<Appointment>().GetAllAsync();
        return appointments.Any(a =>
            a.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase) &&
            a.DateTime == dateTime);
    }
}


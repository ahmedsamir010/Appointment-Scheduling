using Application.DTOs.Request;
using Application.Repositories;
using Application.ResultPattern;
using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace Infrastructre.Services;

public class AppointmentService(IUnitOfWork unitOfWork) : BaseService<Appointment>(unitOfWork), IAppointmentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> CreateAppointmentAsync(CreateAppointmentRequest dto)
    {
        if (dto.DateTime < DateTime.UtcNow)
        {
            return Result.FailureResult("The appointment time cannot be in the past.");
        }

        if (await IsDuplicateAsync(dto.CustomerName, dto.DateTime))
        {
            return Result.FailureResult("An appointment already exists for this customer at the specified time.");
        }

        var appointment = dto.Adapt<Appointment>();

        await _unitOfWork.Repository<Appointment>().AddAsync(appointment);
        await _unitOfWork.CompleteAsync();

        return Result.SuccessResult("Appointment created successfully.");
    }


    public async Task<Result> UpdateAppointmentAsync(int Id, UpdateAppointmentRequest dto)
    {
        var repo = _unitOfWork.Repository<Appointment>();
        var existing = await repo.GetByIdAsync(Id);

        if (existing == null)
        {
            return Result.FailureResult("Appointment not found.");
        }

        if (existing.Status != AppointmentStatus.Scheduled)
        {
            return Result.FailureResult("Only Scheduled appointments can be updated.");
        }

        if (dto.DateTime < DateTime.UtcNow)
        {
            return Result.FailureResult("The requested appointment date cannot be in the past.");
        }

        if (await IsDuplicateAsync(dto.CustomerName, dto.DateTime, Id))
        {
            return Result.FailureResult("A duplicate appointment exists for this customer and date.");
        }

        dto.Adapt(existing);

        await repo.UpdateAsync(existing);
        await _unitOfWork.CompleteAsync();

        return Result.SuccessResult("Appointment updated successfully.");
    }

    public async Task<bool> IsDuplicateAsync(string customerName, DateTime dateTime, int? id = null)
    {
        var normalizedDateTime = NormalizeDateTime(dateTime);

        var query = _unitOfWork.Repository<Appointment>().GetQueryable()
            .AsNoTracking()
            .Where(a => a.CustomerName.ToLower() == customerName.ToLower());

        if (id.HasValue)
        {
            query = query.Where(a => a.Id != id.Value);
        }

        var appointments = await query.ToListAsync();
        return appointments.Any(a => NormalizeDateTime(a.DateTime) == normalizedDateTime);
    }
    private static DateTime NormalizeDateTime(DateTime dt) => dt.AddSeconds(-dt.Second).AddMilliseconds(-dt.Millisecond);
}
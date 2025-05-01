using Application.RequestFilters;

namespace API.Controllers;
/// <summary>
/// Controller for managing appointments.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IAppointmentService appointmentService) : ControllerBase
{
    /// <summary>
    /// Gets all appointments.
    /// </summary>
    /// <returns>List of appointments.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> Get([FromQuery] RequestFilter requestFilter)
    {
        var appointments = await appointmentService.GetAllAsync(requestFilter);
        return Ok(appointments);
    }

    /// <summary>
    /// Gets a specific appointment by ID.
    /// </summary>
    /// <param name="id">The ID of the appointment.</param>
    /// <returns>Appointment details.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<AppointmentResponse>> Get(int id)
    {
        var appointment = await appointmentService.GetByIdAsync(id);
        if (appointment is null)
            return NotFound(new ApiResponse(404));

        var mappedEntity = appointment.Adapt<AppointmentResponse>();
        return Ok(mappedEntity);
    }

    /// <summary>
    /// Creates a new appointment.
    /// </summary>
    /// <param name="createAppointmentRequest">The appointment creation request data.</param>
    /// <returns>Status of the creation process.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentRequest createAppointmentRequest)
    {
        var result = await appointmentService.CreateAppointmentAsync(createAppointmentRequest);
        if (!result)
        {
            return BadRequest(new ApiResponse(400));
        }
        return Ok(new ApiResponse(200));
    }

    /// <summary>
    /// Deletes an appointment by ID.
    /// </summary>
    /// <param name="Id">The ID of the appointment to delete.</param>
    /// <returns>Status of the deletion process.</returns>
    [HttpDelete]
    public async Task<IActionResult> Delete(int Id)
    {
        var entity = await appointmentService.GetByIdAsync(Id);
        if (entity is null) return NotFound(new ApiResponse(404));
        await appointmentService.DeleteAsync(entity);
        return Ok(new ApiResponse(200));
    }

    /// <summary>
    /// Updates an existing appointment.
    /// </summary>
    /// <param name="updateAppointmentRequest">The appointment update request data.</param>
    /// <returns>Status of the update process.</returns>
    [HttpPut]
    public async Task<IActionResult> Update(UpdateAppointmentRequest updateAppointmentRequest)
    {
        var entity = await appointmentService.GetByIdAsync(updateAppointmentRequest.Id);
        if (entity is null) return NotFound(new ApiResponse(404));
        var result = await appointmentService.UpdateAppointmentAsync(updateAppointmentRequest);
        if (result is false) return BadRequest(new ApiResponse(400));
        return Ok(new ApiResponse(200));
    }
}

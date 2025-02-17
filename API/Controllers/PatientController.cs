using Core.DTOs;
using Core.Common.Extensions;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Services;
using Swashbuckle.AspNetCore.Annotations;
using Core.Entities;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Produces("application/json")]
[SwaggerTag("Patient handling controller")]
public class PatientController : ControllerBase
{
    private readonly IPatientService PatientService;

    public PatientController(IPatientService patientService)
    {
        PatientService = patientService;
    }

    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get a patient by ID",
        Description = "Returns the patient by patient identifier"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully found a patient", typeof(Patient))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> FindAsync(Guid id)
    {
        var patient = await PatientService.FindAsync(id);
        return Ok(patient);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all patients",
        Description = "Returns a list of all patients"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved patients", typeof(List<Patient>))]
    public async Task<IActionResult> Find()
    {
        var patients = await PatientService.FindAsync();
        return Ok(patients);
    }

    [HttpGet("search")]
    [SwaggerOperation(
        Summary = "Search patients by birthdate",
        Description = "Finds patients by birthdate and optional birth time"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully retrieved matching patients", typeof(List<Patient>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid search parameters")]
    public async Task<ActionResult<List<Response>>> SearchByBirthDate(
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly time,
        [FromQuery] string dateOp = "eq",
        [FromQuery] string timeOp = "eq")
    {
        var result = await PatientService.FindAsync(dateOp, date, timeOp, time);
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new patient",
        Description = "Creates a new patient record"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "Successfully created a patient", typeof(Patient))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid patient data")]
    public async Task<IActionResult> Create([FromBody] Create dto)
    {
        var result = await PatientService.CreateAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update a patient",
        Description = "Updates an existing patient record"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Successfully updated the patient", typeof(Patient))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid update request")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Update dto)
    {
        dto.Id = id;
        var result = await PatientService.UpdateAsync(dto);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation(
       Summary = "Delete a patient",
       Description = "Deletes a patient record by ID"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Successfully deleted the patient")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Patient not found")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await PatientService.DeleteAsync(id);
        return NoContent();
    }
}

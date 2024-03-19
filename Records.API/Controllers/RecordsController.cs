using System.Net;
using Microsoft.AspNetCore.Mvc;
using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;

namespace Records.API.Controllers;

[ApiController]
[Route("records")]
public class RecordsController(ICreateRecordUseCase createRecordUseCase) : ControllerBase
{
    private readonly ICreateRecordUseCase _createRecordUseCase = createRecordUseCase;
  
    [HttpPost]
    public IActionResult CreateRecord([FromBody] RecordDTO dto)
    {
        var id = _createRecordUseCase.Execute(dto);
        if (id == null) return BadRequest("Record already exists.");
        Response.Headers.Location = $"/Records/{id}";
        return new ObjectResult(id.ToString()){StatusCode = (int)HttpStatusCode.Created};
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(DateTime.Now);
    }
}

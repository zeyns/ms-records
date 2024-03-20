using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;

namespace Records.API.Controllers;

[ApiController]
[Route("records")]
public class RecordsController(ICreateRecordUseCase createRecordUseCase, IGetDateRecordsInfoUseCase getDateRecordsInfoUseCase) : ControllerBase
{
    private readonly ICreateRecordUseCase _createRecordUseCase = createRecordUseCase;

    private readonly IGetDateRecordsInfoUseCase _getDateRecordsInfoUseCase = getDateRecordsInfoUseCase;

    [HttpPost]
    public IActionResult CreateRecord([FromBody] RecordDTO dto)
    {
        var date = _createRecordUseCase.Execute(dto);
        return new ObjectResult(date.ToString()){StatusCode = (int)HttpStatusCode.Created};
    }

    [HttpGet("detail")]
    public IActionResult GetDateInfoRecords([FromQuery] DateOnly date)
    {
        var dayInfoRecords = _getDateRecordsInfoUseCase.Execute(date);
        return Ok(dayInfoRecords);
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(DateTime.Now);
    }
}

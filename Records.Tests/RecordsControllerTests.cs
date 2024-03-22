using Microsoft.AspNetCore.Mvc;
using Moq;
using Records.API.Controllers;
using Records.API.Middlewares;
using Records.Application.Interfaces.Records;
using Records.Domain.DTOs;
using System.Net;

namespace Records.API.Tests;
public class RecordsControllerTests
{
    private readonly Mock<ICreateRecordUseCase> _createRecordUseCaseMock = new Mock<ICreateRecordUseCase>();
    private readonly Mock<IGetDateUserRecordsInfoUseCase> _getDateUserRecordsInfoUseCaseMock = new Mock<IGetDateUserRecordsInfoUseCase>();
    private readonly Mock<IGetLastMonthlyUserRecordsReports> _getLastMonthlyUserRecordsReportsMock = new Mock<IGetLastMonthlyUserRecordsReports>();

    private readonly RecordsController _controller;

    public RecordsControllerTests()
    {
        _controller = new RecordsController(
            _createRecordUseCaseMock.Object,
            _getDateUserRecordsInfoUseCaseMock.Object,
            _getLastMonthlyUserRecordsReportsMock.Object
        );
    }

    [Fact]
    public void CreateRecord_DeveRetornarCreated()
    {
        // Arrange
        var recordDto = new RecordDTO();
        _createRecordUseCaseMock.Setup(x => x.Execute(recordDto)).Returns(DateTime.Now);

        // Act
        AuthMiddleware._userId = Guid.NewGuid();
        var result = _controller.CreateRecord() as ObjectResult;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetDateInfoRecords_DeveRetornarOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var date = new DateOnly();
        _getDateUserRecordsInfoUseCaseMock.Setup(x => x.Execute(userId, date)).Returns(new DateRecordsInfoDTO());

        // Act
        var result = _controller.GetDateInfoRecords(date) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void GetMonthlyUserRecordsReports_DeveRetornarOk()
    {
        // Arrange
        var userId = Guid.NewGuid();
        _getLastMonthlyUserRecordsReportsMock.Setup(x => x.Execute(userId)).Returns(new MonthlyRecordsReport());

        // Act
        var result = _controller.GetMonthlyUserRecordsReports(userId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public void Health_DeveRetornarOk()
    {
        // Act
        var result = _controller.Health() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.IsType<DateTime>(result.Value);
    }
}

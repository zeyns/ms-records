using Microsoft.EntityFrameworkCore;
using Records.Domain.Interfaces;
using Records.Infrastructure.Contexts;
using Records.Infrastructure.Repositories;

namespace Records.Repository.Tests;

public class RecordsRepositoryTests : IDisposable
{
    private readonly DbContextOptions<RecordsContext> _options;
    private readonly RecordsContext _context;
    private readonly IRecordsRepository _repository;

    public RecordsRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<RecordsContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RecordsContext(_options);
        _repository = new RecordsRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void Create_DeveRetornarDataDoRegistro()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var result = _repository.Create(userId);

        // Assert
        Assert.IsType<DateTime>(result);
    }

    [Fact]
    public void GetAllByUserIdAndDate_DeveRetornarListaDeRegistrosPorUsuarioEData()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var date = new DateOnly();
        var recordsList = new List<Domain.Entities.Record>
        {
            new Domain.Entities.Record(userId, DateTime.Now.AddDays(-1)),
            new Domain.Entities.Record(userId, DateTime.Now),
            new Domain.Entities.Record(Guid.NewGuid(), DateTime.Now),
        };

        // Adicionar registros ao contexto em memória
        _context.Record.AddRange(recordsList);
        _context.SaveChanges();

        // Act
        var result = _repository.GetAllByUserIdAndDate(userId, date);

        // Assert
        Assert.Equal(0, result.Count);
    }

    [Fact]
    public void GetAllByUserIdAndMonth_DeveRetornarListaDeRegistrosPorUsuarioEMes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var month = DateTime.Now.Month;
        var recordsList = new List<Domain.Entities.Record>
        {
            new Domain.Entities.Record(userId, DateTime.Now.AddMonths(-1)),
            new Domain.Entities.Record(userId, DateTime.Now),
            new Domain.Entities.Record(Guid.NewGuid(), DateTime.Now),
        };

        // Adicionar registros ao contexto em memória
        _context.Record.AddRange(recordsList);
        _context.SaveChanges();

        // Act
        var result = _repository.GetAllByUserIdAndMonth(userId, month);

        // Assert
        Assert.Equal(1, result.Count);
    }
}
using Microsoft.EntityFrameworkCore;
using Records.Domain.Entities;

namespace Records.Infrastructure.Contexts;

public class RecordsContext(DbContextOptions<RecordsContext> options) : DbContext(options)
{
    public DbSet<Record> Record { get; set; } = null!;
}
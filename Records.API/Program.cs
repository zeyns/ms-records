using Records.Application.Interfaces.Records;
using Records.Application.UseCases.Records;
using Records.Domain.Interfaces;
using Records.Infrastructure.Contexts;
using Records.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IRecordsRepository, RecordsRepository>();
builder.Services.AddScoped<ICreateRecordUseCase, CreateRecordUseCase>();
builder.Services.AddScoped<IGetRecordByDocumentUseCase, GetRecordByDocumentUseCase>();
builder.Services.AddScoped<IGetRecordByRecordIdUseCase, GetRecordByRecordIdUseCase>();
builder.Services.AddScoped<IGetRecordsUseCase, GetRecordsUseCase>();
builder.Services.AddScoped<IDeleteRecordUseCase, DeleteRecordUseCase>();
builder.Services.AddDbContext<RecordsContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<RecordsContext>();
    dataContext.Database.Migrate();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();


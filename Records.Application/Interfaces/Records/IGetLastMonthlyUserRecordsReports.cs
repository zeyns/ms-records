using Records.Application.Interfaces;
using Records.Domain.DTOs;

public interface IGetLastMonthlyUserRecordsReports: IUseCase<MonthlyRecordsReport?, Guid>
{
}
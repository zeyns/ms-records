namespace Records.Application.Interfaces;
public interface IUseCase<out TResponse, in TRequest>
{
    TResponse Execute(TRequest args);
}
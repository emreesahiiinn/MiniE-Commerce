namespace Core.Entities.Abstract;

public interface IDataResult<T> : IResult
{
    T Data { get; }
}
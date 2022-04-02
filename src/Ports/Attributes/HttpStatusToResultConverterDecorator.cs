using System.Net;
using MinimalBFF.Domain.Responses;
using Paramore.Darker;

namespace MinimalBFF.Ports.Attributes;

public class HttpStatusToResultConverterDecorator<TQuery, TResult> : IQueryHandlerDecorator<TQuery, TResult>
    where TQuery : IQuery<TResult>
{
    public IQueryContext Context { get; set; }
    
    public void InitializeFromAttributeParams(object[] attributeParams)
    {
        // nothing to do
    }
    
    public TResult Execute(TQuery query, Func<TQuery, TResult> next, Func<TQuery, TResult> fallback)
    {
        var result = next(query);

        return ProcessResult(result);
    }

    public async Task<TResult> ExecuteAsync(TQuery query, Func<TQuery, CancellationToken, Task<TResult>> next, Func<TQuery, CancellationToken, Task<TResult>> fallback,
        CancellationToken cancellationToken = new())
    {
        var result = await next(query, cancellationToken);

        return ProcessResult(result);
    }

    private static TResult ProcessResult(TResult result)
    {
        if (result is not IWeatherResponse weatherResponse)
            return result;

        weatherResponse.Result = weatherResponse.StatusCode switch
        {
            HttpStatusCode.OK => Results.Ok(weatherResponse.Data),
            HttpStatusCode.NotFound => Results.NotFound(),
            HttpStatusCode.Unauthorized => Results.Unauthorized(),
            _ => throw new ApplicationException()
        };

        return result;
    }
}
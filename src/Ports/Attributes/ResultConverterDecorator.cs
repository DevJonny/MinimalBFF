using System.ComponentModel.DataAnnotations;
using System.Net;
using MinimalBFF.Domain.Responses;
using Paramore.Darker;

namespace MinimalBFF.Ports.Attributes;

public class ResultConverterDecorator<TQuery, TResult> : IQueryHandlerDecorator<TQuery, TResult>
    where TQuery : IQuery<TResult> where TResult : class
{
    public IQueryContext Context { get; set; }
    
    public void InitializeFromAttributeParams(object[] attributeParams)
    {
        // Nothing to do
    }
    
    public TResult Execute(TQuery query, Func<TQuery, TResult> next, Func<TQuery, TResult> fallback)
        => throw new NotImplementedException("Synchronous handling is not supported");

    public async Task<TResult> ExecuteAsync(TQuery query, Func<TQuery, CancellationToken, Task<TResult>> next, Func<TQuery, CancellationToken, Task<TResult>> fallback,
        CancellationToken cancellationToken = new())
    {
        TResult result = null;
        
        try
        {
            result = await next(query, cancellationToken);

            if (result is not IWeatherResponse weatherResponse)
                return result;

            weatherResponse.Result = result switch
            {
                IWeatherCreatedResponse createdResponse => Results.Created(createdResponse.ContentLocation, createdResponse),
                IWeatherAcceptedResponse acceptedResponse => Results.Accepted(acceptedResponse.Location, acceptedResponse),
                _ => Results.Ok(weatherResponse)
            };

            return result;
        }
        catch (ValidationException e)
        {
            var weatherResponse = new WeatherResponse { Result = Results.BadRequest(e.Message) };
            return weatherResponse as TResult;
        }
        catch (HttpRequestException e)
        {
            var weatherResponse = new WeatherResponse
            {
                Result = e.StatusCode switch
                {
                    HttpStatusCode.NotFound => Results.NotFound(),
                    HttpStatusCode.Unauthorized => Results.Unauthorized(),
                    HttpStatusCode.BadRequest => Results.BadRequest(),
                    _ => throw new AggregateException()
                }
            };

            return weatherResponse as TResult;
        }
    }
}
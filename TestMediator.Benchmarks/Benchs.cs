using MediatR;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace TestMediator.Benchmarks;

[MemoryDiagnoser(false)]
public class Benchs
{
    private readonly MediatR.IMediator _mediatr;
    private readonly global::Mediator.IMediator _mediator;

    public Benchs()
    {
        var oldServiceColl = new ServiceCollection();
        oldServiceColl.AddMediatR(config => config.AsTransient(),
            typeof(TestMediator.CurrentMediatR.Weather.WeatherForecast));
        var oldProvider = oldServiceColl.BuildServiceProvider();
        _mediatr = oldProvider.GetRequiredService<IMediator>();

        var newServiceCollection = new ServiceCollection();
        newServiceCollection.AddMediator();
        var newProvider = newServiceCollection.BuildServiceProvider();
        _mediator = newProvider.GetRequiredService<global::Mediator.IMediator>();
    }

    [Benchmark]
    public async Task<CurrentMediatR.Weather.WeatherForecast[]> Weather_Old()
    {
        var request = CurrentMediatR.Weather.WeatherForecastRequest.Instance;
        var results = await _mediatr.Send(request);
        return results.ToArray();
    }

    [Benchmark]
    public async Task<NewMediator.Weather.WeatherForecast[]> Weather_New()
    {
        var request = NewMediator.Weather.WeatherForecastRequest.Instance;
        var results = await _mediator.Send(request);
        return results.ToArray();
    }
}
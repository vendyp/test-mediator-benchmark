using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace TestMediator.CurrentMediatR.Weather;

public class WeatherForecastHandler : IRequestHandler<WeatherForecastRequest, IEnumerable<WeatherForecast>>
{
    public WeatherForecastHandler(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private readonly IServiceProvider serviceProvider;

    public Task<IEnumerable<WeatherForecast>> Handle(WeatherForecastRequest request,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<DataService>();
        return Task.FromResult(service.GetData());
    }
}
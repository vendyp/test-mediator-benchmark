using Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace TestMediator.NewMediator.Weather;

public class WeatherForecastHandler : IRequestHandler<WeatherForecastRequest, IEnumerable<WeatherForecast>>
{
    public WeatherForecastHandler(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    private readonly IServiceProvider serviceProvider;

    public ValueTask<IEnumerable<WeatherForecast>> Handle(WeatherForecastRequest request,
        CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<DataService>();
        return ValueTask.FromResult(service.GetData());
    }
}
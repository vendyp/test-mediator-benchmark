﻿using Mediator;

namespace TestMediator.NewMediator.Weather;

public record WeatherForecastRequest : IRequest<IEnumerable<WeatherForecast>>
{
    public static readonly WeatherForecastRequest Instance = new();
    
    private WeatherForecastRequest()
    {
    }
}
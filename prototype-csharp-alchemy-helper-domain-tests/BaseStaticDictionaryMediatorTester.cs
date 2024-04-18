using prototype_csharp_alchemy_helper_domain;
using Microsoft.Extensions.Logging;

namespace prototype_csharp_alchemy_helper_domain_tests;

public class BaseStaticDictionaryMediatorTester
{
    protected StaticDictionaryMediator _mediator { get; private set; }

    public BaseStaticDictionaryMediatorTester()
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger<StaticDictionaryMediator> logger = loggerFactory.CreateLogger<StaticDictionaryMediator>();
        this._mediator = new StaticDictionaryMediator(logger);
    }
}

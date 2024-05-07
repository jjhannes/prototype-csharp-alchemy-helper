using prototype_csharp_alchemy_helper_domain;
using Microsoft.Extensions.Logging;
using prototype_csharp_alchemy_helper_datastore;

namespace prototype_csharp_alchemy_helper_domain_tests;

public class BaseStaticDictionaryMediatorWithOblivionRepoTester
{
    protected StaticDictionaryMediator _mediator { get; private set; }

    public BaseStaticDictionaryMediatorWithOblivionRepoTester(int level)
    {
        ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        ILogger<StaticDictionaryMediator> logger = loggerFactory.CreateLogger<StaticDictionaryMediator>();
        this._mediator = new StaticDictionaryMediator(logger, new OblivionRepo(level));
    }
}

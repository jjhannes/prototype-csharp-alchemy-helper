using prototype_csharp_alchemy_helper_domain;

namespace prototype_csharp_alchemy_helper_domain_tests;

public class BaseStaticDictionaryMediatorTester
{
    protected StaticDictionaryMediator _mediator { get; private set; }

    public BaseStaticDictionaryMediatorTester()
    {
        this._mediator = new StaticDictionaryMediator();
    }
}

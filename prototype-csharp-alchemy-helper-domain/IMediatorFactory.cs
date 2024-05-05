namespace prototype_csharp_alchemy_helper_domain;

public interface IMediatorFactory
{
    IMediator GetMediator(string? url);
}

namespace prototype_csharp_alchemy_helper_api;

public class CollectionResponse<T>
{
    public int Count { get; }

    public IEnumerable<T> Items { get; }

    public CollectionResponse(T[] collection)
    {
        this.Count = collection.Count();
        this.Items = collection;
    }

    public CollectionResponse(IEnumerable<T> collection)
    {
        this.Count = collection.Count();
        this.Items = collection;
    }
}

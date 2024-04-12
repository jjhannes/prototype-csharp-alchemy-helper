

internal partial class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");
    }
}
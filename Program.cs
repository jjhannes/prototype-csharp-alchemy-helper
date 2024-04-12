

internal partial class Program
{
    private static void Main(string[] args)
    {
        var data = IngredientEffects.data;
        Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");
    }
}
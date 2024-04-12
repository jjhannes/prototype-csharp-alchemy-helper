

internal partial class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");

        string rawEbonyIngredient = "Raw Ebony";
        string[] effectsWaterWalkingAndFortifySpeed = [ "Water Walking", "Fortify Speed" ];

        string[] rawEbonyEffects = Program.GetEffectsForIngredient(rawEbonyIngredient);
        string[] ingredientsWithWaterWalkingAndFortifySpeed = GetIngredientsWithEffects(effectsWaterWalkingAndFortifySpeed);

        Console.WriteLine($"{rawEbonyIngredient} has effects [{string.Join(", ", rawEbonyEffects)}]");
        Console.WriteLine($"Ingredients with effects [{string.Join(" & ", effectsWaterWalkingAndFortifySpeed)}] are [{string.Join(", ", ingredientsWithWaterWalkingAndFortifySpeed)}]");
    }

    private static string[] GetEffectsForIngredient(string ingredient)
    {
        return data
            .Single(i => i.Key == ingredient)
            .Value;
    }

    private static string[] GetIngredientsWithEffects(string[] effects)
    {
        return data
            .Where(i => i.Value.Intersect(effects).Any())
            .Select(i => i.Key)
            .ToArray();
    }
}
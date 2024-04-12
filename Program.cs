

internal partial class Program
{
    private static void Main(string[] args)
    {
        // Check data structure
        Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");

        // Test get ingredient effects & get ingredients with effects
        string rawEbonyIngredient = "Raw Ebony";
        string[] effectsWaterWalkingAndFortifySpeed = [ "Water Walking", "Fortify Speed" ];

        string[] rawEbonyEffects = Program.GetEffectsForIngredient(rawEbonyIngredient);
        string[] ingredientsWithWaterWalkingAndFortifySpeed = GetIngredientsWithEffects(effectsWaterWalkingAndFortifySpeed);

        Console.WriteLine($"{rawEbonyIngredient} has effects [{string.Join(", ", rawEbonyEffects)}]");
        Console.WriteLine($"Ingredients with effects [{string.Join(" & ", effectsWaterWalkingAndFortifySpeed)}] are [{string.Join(", ", ingredientsWithWaterWalkingAndFortifySpeed)}]");

        // Test is bad effect
        string[] badEffectIngredients = data
            .Where(i => i.Value.Any(e => IsBadEffect(e)))
            .Select(i => i.Key)
            .ToArray();
        string[] badEffects = data
            .SelectMany(i => i.Value)
            .Where(e => IsBadEffect(e))
            .Distinct()
            .ToArray();
        Console.WriteLine($"{badEffectIngredients.Count()} of {data.Count} ingredients have bad effects, and {badEffects.Count()} of {data.SelectMany(i => i.Value).Distinct().Count()} effects are bad.");
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

    private static bool IsBadEffect(string effect)
    {
        effect = effect.ToLower();

        if (effect.Contains("cure") || effect.Contains("resist"))
        {
            return false;
        }

        return effect.Contains("burden") ||
            effect.Contains("poison") ||
            effect.Contains("blind") ||
            effect.Contains("damage") ||
            effect.Contains("drain") ||
            effect.Contains("paralyz") ||
            effect.Contains("weakness") ||
            effect.Contains("vampirism");
    }
}
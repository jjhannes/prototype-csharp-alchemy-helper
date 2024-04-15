namespace prototype_csharp_alchemy_helper_console;

using prototype_csharp_alchemy_helper_domain;

internal partial class Program
{
    private static IMediator _mediator = new StaticDictionaryMediator();

    private static void Main(string[] args)
    {
        Program.PromptDesiredEffectsAndCalculateRecipe();
    }

    private static void PromptDesiredEffectsAndCalculateRecipe()
    {
        Program.PromptDescription();

        List<string> desiredEffects = new List<string>();
        bool done = false;
        string? input = null;

        while (!done)
        {
            input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                // Done OR empty input
                if (desiredEffects.Count < 1)
                {
                    // Empty input
                    Program.PromptEmptyInput();

                    continue;
                }
                else
                {
                    done = true;

                    continue;
                }
            }
            else
            {
                desiredEffects.Add(input);
            }
        }
        
        IEnumerable<Recipe> possibleRecipes = Program._mediator.DetermineRecipe(desiredEffects.ToArray());

        if (possibleRecipes.Count() < 1)
        {
            Console.WriteLine($"There are no recipes that will grant [{string.Join(" & ", desiredEffects)}]");
        }
        else
        {
            Console.WriteLine($"There are {possibleRecipes.Count()} recipes that will grant [{string.Join(" & ", desiredEffects)}]:");
            Console.WriteLine();

            foreach (Recipe recipe in possibleRecipes)
            {
                Console.WriteLine($"{recipe}");
                Console.WriteLine($"");
            }
        }
    }

    private static void PromptDescription()
    {
        Console.WriteLine("What effects would you like your potion to have?");
        Console.WriteLine("(Press <Enter> to submit and provide additional desired effects)");
        Console.WriteLine("(Press <Enter> again, i.e. empty value, to accept your selection)");
    }

    private static void PromptEmptyInput()
    {
        Console.WriteLine($"");
        Console.WriteLine("You've not provided any desired effects. Please provide a desired effect.");
    }

    // private static void RunTests()
    // {
    //     Dictionary<string, string[]> data = IngredientEffects.GetEverything();

    //     // Check data structure
    //     Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");

    //     // Test get ingredient effects & get ingredients with effects
    //     string rawEbonyIngredient = "Raw Ebony";
    //     string[] effectsWaterWalkingAndFortifySpeed = [ "Water Walking", "Fortify Speed" ];

    //     string[] rawEbonyEffects = IngredientEffects.GetIngredientEffects(rawEbonyIngredient);
    //     string[] ingredientsWithWaterWalkingAndFortifySpeed = IngredientEffects.GetIngredientsWithEffects(effectsWaterWalkingAndFortifySpeed);

    //     Console.WriteLine($"{rawEbonyIngredient} has effects [{string.Join(", ", rawEbonyEffects)}]");
    //     Console.WriteLine($"Ingredients with effects [{string.Join(" & ", effectsWaterWalkingAndFortifySpeed)}] are [{string.Join(", ", ingredientsWithWaterWalkingAndFortifySpeed)}]");

    //     // Test is bad effect
    //     string[] badEffectIngredients = data
    //         .Where(i => i.Value.Any(e => Repo.IsBadEffect(e)))
    //         .Select(i => i.Key)
    //         .ToArray();
    //     string[] badEffects = data
    //         .SelectMany(i => i.Value)
    //         .Where(e => Repo.IsBadEffect(e))
    //         .Distinct()
    //         .ToArray();
    //     Console.WriteLine($"{badEffectIngredients.Count()} of {data.Count} ingredients have bad effects, and {badEffects.Count()} of {data.SelectMany(i => i.Value).Distinct().Count()} effects are bad.");

    //     // Test get common effects
    //     string[] ingredientsScalesAndKwamaCuttle = [ "Scales", "Kwama Cuttle" ];
    //     string[] commonEffectsForScalesAndKwamaCuttle = Program.GetCommonEffects(ingredientsScalesAndKwamaCuttle);

    //     Console.WriteLine($"[{string.Join(" & ", ingredientsScalesAndKwamaCuttle)}] has common effects [{(commonEffectsForScalesAndKwamaCuttle.Any() ? string.Join(", ", commonEffectsForScalesAndKwamaCuttle) : "None")}]");
    // }

}
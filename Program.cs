﻿

internal partial class Program
{
    private static void Main(string[] args)
    {
        // Program.RunTests();
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
                if (!ValidateDesiredEffect(input))
                {
                    Program.PromptInvalidInput(input);

                    continue;
                }
                else
                {
                    desiredEffects.Add(input);
                }
            }
        }
        
        string[][] possibleRecipes = Program.DetermineRecipe(desiredEffects.ToArray());

        Console.WriteLine($"Possible recipes for potions with [{string.Join(" & ", desiredEffects)}]:");

        foreach (string[] recipe in possibleRecipes)
        {
            Console.WriteLine($" - {string.Join(" + ", recipe)}");
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

    private static bool ValidateDesiredEffect(string effect)
    {
        return data
            .SelectMany(i => i.Value)
            .Distinct()
            .Any(e => e == effect);
    }

    private static void PromptInvalidInput(string input)
    {
        Console.WriteLine($"");
        Console.WriteLine($"{input} is not a valid effect. Please provide a valid desired effect.");
    }

    private static void RunTests()
    {
        // Check data structure
        Console.WriteLine($"There are {data.Count} ingredients with a total of {data.SelectMany(i => i.Value).Distinct().Count()} unique effects");

        // Test get ingredient effects & get ingredients with effects
        string rawEbonyIngredient = "Raw Ebony";
        string[] effectsWaterWalkingAndFortifySpeed = [ "Water Walking", "Fortify Speed" ];

        string[] rawEbonyEffects = Program.GetEffectsForIngredient(rawEbonyIngredient);
        string[] ingredientsWithWaterWalkingAndFortifySpeed = Program.GetIngredientsWithEffects(effectsWaterWalkingAndFortifySpeed);

        Console.WriteLine($"{rawEbonyIngredient} has effects [{string.Join(", ", rawEbonyEffects)}]");
        Console.WriteLine($"Ingredients with effects [{string.Join(" & ", effectsWaterWalkingAndFortifySpeed)}] are [{string.Join(", ", ingredientsWithWaterWalkingAndFortifySpeed)}]");

        // Test is bad effect
        string[] badEffectIngredients = data
            .Where(i => i.Value.Any(e => Program.IsBadEffect(e)))
            .Select(i => i.Key)
            .ToArray();
        string[] badEffects = data
            .SelectMany(i => i.Value)
            .Where(e => Program.IsBadEffect(e))
            .Distinct()
            .ToArray();
        Console.WriteLine($"{badEffectIngredients.Count()} of {data.Count} ingredients have bad effects, and {badEffects.Count()} of {data.SelectMany(i => i.Value).Distinct().Count()} effects are bad.");

        // Test get common effects
        string[] ingredientsScalesAndKwamaCuttle = [ "Scales", "Kwama Cuttle" ];
        string[] commonEffectsForScalesAndKwamaCuttle = Program.GetCommonEffects(ingredientsScalesAndKwamaCuttle);

        Console.WriteLine($"[{string.Join(" & ", ingredientsScalesAndKwamaCuttle)}] has common effects [{(commonEffectsForScalesAndKwamaCuttle.Any() ? string.Join(", ", commonEffectsForScalesAndKwamaCuttle) : "None")}]");
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

    private static string[] GetCommonEffects(string[] ingredients)
    {
        List<KeyValuePair<string, string[]>> filteredIngredients = data
            .Where(i => ingredients.Contains(i.Key))
            .ToList();

        List<string> commonEffects = new List<string>();

        for (int primary = 0; primary < filteredIngredients.Count; primary++)
        {
            KeyValuePair<string, string[]> primaryIngredient = filteredIngredients[primary];

            for (int secondary = primary + 1; secondary < filteredIngredients.Count; secondary++)
            {
                KeyValuePair<string, string[]> secondaryIngredient = filteredIngredients[secondary];

                string[] matches = primaryIngredient.Value.Intersect(secondaryIngredient.Value).ToArray();

                foreach (string match in matches)
                {
                    if (!commonEffects.Contains(match))
                    {
                        commonEffects.Add(match);
                    }
                }
            }
        }

        return commonEffects.ToArray();
    }
    
    private static string[][] DetermineRecipe(string[] desiredEffects)
    {
        List<string[]> recipe = new List<string[]>();
        string[] possibleIngredients = Program.GetIngredientsWithEffects(desiredEffects);

        // Two ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                var commonEffects = Program.GetCommonEffects([ primaryIngredient, secondaryIngredient ]);

                if (desiredEffects.All(de => commonEffects.Contains(de)))
                {
                    recipe.Add([ primaryIngredient, secondaryIngredient ]);
                }
            }
        }

        // Three ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                
                for (int tertiary = secondary + 1; tertiary < possibleIngredients.Count(); tertiary++)
                {
                    string tertiaryIngredient = possibleIngredients[tertiary];
                    var commonEffects = Program.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient ]);

                    if (desiredEffects.All(de => commonEffects.Contains(de)))
                    {
                        recipe.Add([ primaryIngredient, secondaryIngredient, tertiaryIngredient ]);
                    }
                }
            }
        }

        // Four ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                
                for (int tertiary = secondary + 1; tertiary < possibleIngredients.Count(); tertiary++)
                {
                    string tertiaryIngredient = possibleIngredients[tertiary];
                    
                    for (int quaternary = tertiary + 1; quaternary < possibleIngredients.Count(); quaternary++)
                    {
                        string quaternaryIngredient = possibleIngredients[quaternary];
                        var commonEffects = Program.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ]);

                        if (desiredEffects.All(de => commonEffects.Contains(de)))
                        {
                            recipe.Add([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ]);
                        }
                    }
                }
            }
        }

        return recipe.ToArray();
    }
}
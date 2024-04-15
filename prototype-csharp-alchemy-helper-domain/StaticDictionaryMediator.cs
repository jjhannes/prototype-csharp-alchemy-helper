namespace prototype_csharp_alchemy_helper_domain;

using prototype_csharp_alchemy_helper_datastore;

public class StaticDictionaryMediator : IMediator
{
    private readonly IRepo _datastore;

    public StaticDictionaryMediator()
    {
        this._datastore = new StaticDictionaryRepo();
    }

    internal bool IsBadEffect(string effect)
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

    private string[] GetCommonEffects(string[] ingredients)
    {
        List<KeyValuePair<string, string[]>> filteredIngredients = _datastore.GetEverything()
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
    
    private bool IsIngredient(string ingredient)
    {
        return this._datastore.GetEverything()
            .ContainsKey(ingredient);
    }

    private bool IsEffect(string effect)
    {
        return this._datastore.GetEverything()
            .SelectMany(i => i.Value)
            .Distinct()
            .Any(e => e == effect);
    }

    private string[] GetIngredientEffects(string ingredient)
    {
        return this._datastore.GetEverything()[ingredient];
    }

    private string[] GetIngredientsWithEffects(string[] effects)
    {
        return this._datastore.GetEverything()
            .Where(i => i.Value.Intersect(effects).Any())
            .Select(i => i.Key)
            .ToArray();
    }

    public IEnumerable<Recipe> DetermineRecipe(string[] desiredEffects)
    {
        List<Recipe> recipe = new List<Recipe>();
        string[] possibleIngredients = this.GetIngredientsWithEffects(desiredEffects);

        // Two ingredients
        for (int primary = 0; primary < possibleIngredients.Count(); primary++)
        {
            string primaryIngredient = possibleIngredients[primary];

            for (int secondary = primary + 1; secondary < possibleIngredients.Count(); secondary++)
            {
                string secondaryIngredient = possibleIngredients[secondary];
                var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient ]);

                if (desiredEffects.All(de => commonEffects.Contains(de)))
                {
                    recipe.Add(new Recipe([ primaryIngredient, secondaryIngredient ], commonEffects, this.IsBadEffect));
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
                    var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient ]);

                    if (desiredEffects.All(de => commonEffects.Contains(de)))
                    {
                        recipe.Add(new Recipe([ primaryIngredient, secondaryIngredient, tertiaryIngredient ], commonEffects, this.IsBadEffect));
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
                        var commonEffects = this.GetCommonEffects([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ]);

                        if (desiredEffects.All(de => commonEffects.Contains(de)))
                        {
                            recipe.Add(new Recipe([ primaryIngredient, secondaryIngredient, tertiaryIngredient, quaternaryIngredient ], commonEffects, this.IsBadEffect));
                        }
                    }
                }
            }
        }

        return recipe;
    }

}
